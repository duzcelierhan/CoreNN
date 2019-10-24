using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using CoreNN.Tools;
using NUnit.Framework;

#nullable enable

namespace CoreNNTest
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<Pending>")]
    public class Tests
    {
        public const string F2_1023File = @"Files\Float2_1023.txt";
        public const string F2_1024File = @"Files\Float2_1024.txt";
        public const string F2_1M1File = @"Files\Float2_1M-1.txt";
        public const string F2_1MFile = @"Files\Float2_1M.txt";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        [Explicit]
        [TestCase(1023, F2_1023File)]
        [TestCase(1024, F2_1024File)]
        [TestCase(1024*1024-1, F2_1M1File)]
        [TestCase(1024*1024, F2_1MFile)]
        public void FillFileWithRandom(int cnt, string fileName)
        {
            var fa1 = new float[cnt];
            var fa2 = new float[cnt];
            FillFloatArrayWithRandom(ref fa1);
            FillFloatArrayWithRandom(ref fa2);
            WriteToFile(fileName, fa1, fa2);
            Console.WriteLine($"File is filled: {Path.GetFullPath(fileName)}");
        }

        [Test]
        public void MultiplierTest()
        {
            var (fa1, fa2) = Read2ColumnFloatsFromFile(F2_1MFile);
            Console.WriteLine("Starting calculations");
            var sw1 = Stopwatch.StartNew();
            var rIntrFma = Multipliers.DotMultiplyIntrinsicWFma(fa1.AsMemory(), fa2.AsMemory());
            sw1.Stop();
            Console.WriteLine($"Intrinsic calculation (Fma) finished in {sw1.Elapsed}. Result is: {rIntrFma}");

            var sw1ptr = Stopwatch.StartNew();
            var rIntrFmaPtr = Multipliers.DotMultiplyIntrinsicWFma(fa1.AsMemory(), fa2.AsMemory());
            sw1ptr.Stop();
            Console.WriteLine($"Intrinsic calculation (FmaWPtr) finished in {sw1ptr.Elapsed}. Result is: {rIntrFmaPtr}");

            var sw2 = Stopwatch.StartNew();
            var rIntrAvx = Multipliers.DotMultiplyIntrinsicWAvx(fa1.AsMemory(), fa2.AsMemory());
            sw2.Stop();
            Console.WriteLine($"Intrinsic calculation (Avx) finished in {sw2.Elapsed}. Result is: {rIntrAvx}");

            var sw2ptr = Stopwatch.StartNew();
            var rIntrAvxPtr = Multipliers.DotMultiplyIntrinsicWAvx(fa1.AsMemory(), fa2.AsMemory());
            sw2ptr.Stop();
            Console.WriteLine($"Intrinsic calculation (AvxWPtr) finished in {sw2ptr.Elapsed}. Result is: {rIntrAvxPtr}");

            var sw3 = Stopwatch.StartNew();
            var rClas = Multipliers.DotMultiplyClassic(fa1.AsMemory(), fa2.AsMemory());
            sw3.Stop();
            Console.WriteLine($"Classic calculation finished in {sw3.Elapsed}. Result is: {rClas}");

            var sw4 = Stopwatch.StartNew();
            var rClasG4 = Multipliers.DotMultiplyClassicGroup4(fa1.AsMemory(), fa2.AsMemory());
            sw4.Stop();
            Console.WriteLine($"Classic calculation Group 4 finished in {sw4.Elapsed}. Result is: {rClasG4}");

            var sw5 = Stopwatch.StartNew();
            var rClasG8 = Multipliers.DotMultiplyClassicGroup8(fa1.AsMemory(), fa2.AsMemory());
            sw4.Stop();
            Console.WriteLine($"Classic calculation Group 8 finished in {sw5.Elapsed}. Result is: {rClasG8}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void FillFloatArrayWithRandom(ref float[] fArray)
        {
            var rnd = new Random();
            for (var i = 0; i < fArray.Length; i++)
            {
                fArray[i] = (float) rnd.NextDouble();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        public static void WriteToFile(string fileName, float[] numbers1, float[] numbers2)
        {
            using var fs = File.OpenWrite(fileName);
            using var writer = new StreamWriter(fs);
            for (var i = 0; i < numbers1.Length; i++)
            {
                writer.Write(numbers1[i]);
                writer.Write('\t');
                writer.WriteLine(numbers2[i]);
            }
        }

        public static (float[] fa1, float[] fa2) Read2ColumnFloatsFromFile(string file)
        {
            using var fs = File.OpenRead(file);
            using var reader = new StreamReader(fs);
            string? line = null;
            var lst1 = new List<float>();
            var lst2 = new List<float>();
            while ((line = reader.ReadLine()) != null)
            {
                var sp = line.Split('\t');
                var ff = Array.ConvertAll(sp, float.Parse);
                lst1.Add(ff[0]);
                lst2.Add(ff[1]);
            }

            return (lst1.ToArray(), lst2.ToArray());
        }
    }
}