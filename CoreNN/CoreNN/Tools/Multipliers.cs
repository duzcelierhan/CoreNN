using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Text;
using System.Runtime.Intrinsics.X86;

namespace CoreNN.Tools
{
    public class Multipliers
    {
        public static float DotMultiplyIntrinsicWFma(Memory<float> vector1, Memory<float> vector2)
        {
            var span1 = vector1.Span;
            var span2 = vector2.Span;
            var cnt = Math.Min(span1.Length, span2.Length);
            var v3 = Vector256.CreateScalarUnsafe(0f);
#if DEBUG
            var file = Path.GetTempFileName();
            using var writer = new StreamWriter(file);
            Console.WriteLine($"Intrinsic with Fma Mult. results will be written into {file}");
#endif

            int i;
            unsafe
            {
                for (i = 0; i < cnt; i += Vector256<float>.Count)
                {
                    var v1 = Avx.LoadVector256((float*) Unsafe.AsPointer(ref span1[i]));
                    var v2 = Avx.LoadVector256((float*) Unsafe.AsPointer(ref span2[i]));
                    v3 = Fma.MultiplyAdd(v1, v2, v3);
#if DEBUG
                    writer.WriteLine($"{v1.ToString()}\t{v2.ToString()}\t{v3.ToString()}");
#endif
                }
            }

            var rCount = cnt % Vector256<float>.Count;
            var rest1 = span1[^rCount..];
            var rest2 = span2[^rCount..];
            var total = 0f;

            for (i = 0; i < Vector256<float>.Count; i++)
            {
                total += v3.GetElement(i);
            }

            for (i = 0; i < rCount; i++)
            {
                total += rest1[i] * rest2[i];
            }

            if (vector1.Length != vector2.Length)
            {
                Span<float> h, l;
                if (vector1.Length > vector2.Length)
                {
                    h = span1;
                    l = span2;
                }
                else
                {
                    h = span2;
                    l = span1;
                }

                var rest3 = h[l.Length..];
                foreach (var t in rest3)
                {
                    total += t;
                }
            }

            return total;
        }

        public static float DotMultiplyIntrinsicWFmaWSpanPtr(Memory<float> vector1, Memory<float> vector2)
        {
            var span1 = vector1.Span;
            var span2 = vector2.Span;
            var cnt = Math.Min(span1.Length, span2.Length);
            var v3 = Vector256.CreateScalarUnsafe(0f);
            var vectCnt = Vector256<float>.Count;
#if DEBUG
            var file = Path.GetTempFileName();
            using var writer = new StreamWriter(file);
            Console.WriteLine($"Intrinsic with FmaWPtr Mult. results will be written into {file}");
#endif

            int i;
            unsafe
            {
                var ptr1 = (float*) Unsafe.AsPointer(ref span1[0]);
                var ptr2 = (float*) Unsafe.AsPointer(ref span2[0]);

                for (i = 0; i < cnt; i += vectCnt)
                {
                    var v1 = Avx.LoadVector256(ptr1);
                    var v2 = Avx.LoadVector256(ptr2);
                    v3 = Fma.MultiplyAdd(v1, v2, v3);
                    ptr1 += vectCnt;
                    ptr2 += vectCnt;
#if DEBUG
                    writer.WriteLine($"{v1.ToString()}\t{v2.ToString()}\t{v3.ToString()}");
#endif
                }
            }

            var rCount = cnt % Vector256<float>.Count;
            var rest1 = span1[^rCount..];
            var rest2 = span2[^rCount..];
            var total = 0f;

            for (i = 0; i < Vector256<float>.Count; i++)
            {
                total += v3.GetElement(i);
            }

            for (i = 0; i < rCount; i++)
            {
                total += rest1[i] * rest2[i];
            }

            if (vector1.Length != vector2.Length)
            {
                Span<float> h, l;
                if (vector1.Length > vector2.Length)
                {
                    h = span1;
                    l = span2;
                }
                else
                {
                    h = span2;
                    l = span1;
                }

                var rest3 = h[l.Length..];
                foreach (var t in rest3)
                {
                    total += t;
                }
            }

            return total;
        }

        public static float DotMultiplyIntrinsicWAvx(Memory<float> vector1, Memory<float> vector2)
        {
            var span1 = vector1.Span;
            var span2 = vector2.Span;
            var cnt = Math.Min(span1.Length, span2.Length);
            var v3 = Vector256.CreateScalarUnsafe(0f);
#if DEBUG
            var file = Path.GetTempFileName();
            using var writer = new StreamWriter(file);
            Console.WriteLine($"Intrinsic with Avx Mult. results will be written into {file}");
#endif
            int i;
            unsafe
            {
                for (i = 0; i < cnt; i += Vector256<float>.Count)
                {
                    var v1 = Avx.LoadVector256((float*)Unsafe.AsPointer(ref span1[i]));
                    var v2 = Avx.LoadVector256((float*)Unsafe.AsPointer(ref span2[i]));
                    var t = Avx.Multiply(v1, v2);
                    v3 = Avx.Add(v3, t);
#if DEBUG
                    writer.WriteLine($"{v1.ToString()}\t{v2.ToString()}\t{v3.ToString()}");
#endif
                }
            }

            var rCount = cnt % Vector256<float>.Count;
            var rest1 = span1[^rCount..];
            var rest2 = span2[^rCount..];
            var total = 0f;

            for (i = 0; i < Vector256<float>.Count; i++)
            {
                total += v3.GetElement(i);
            }

            for (i = 0; i < rCount; i++)
            {
                total += rest1[i] * rest2[i];
            }

            if (vector1.Length != vector2.Length)
            {
                Span<float> h, l;
                if (vector1.Length > vector2.Length)
                {
                    h = span1;
                    l = span2;
                }
                else
                {
                    h = span2;
                    l = span1;
                }

                var rest3 = h[l.Length..];
                foreach (var t in rest3)
                {
                    total += t;
                }
            }

            return total;
        }

        public static float DotMultiplyIntrinsicWAvxWSpanPtr(Memory<float> vector1, Memory<float> vector2)
        {
            var span1 = vector1.Span;
            var span2 = vector2.Span;
            var cnt = Math.Min(span1.Length, span2.Length);
            var v3 = Vector256.CreateScalarUnsafe(0f);
            var vectCnt = Vector256<float>.Count;
#if DEBUG
            var file = Path.GetTempFileName();
            using var writer = new StreamWriter(file);
            Console.WriteLine($"Intrinsic with AvxWPtr Mult. results will be written into {file}");
#endif
            int i;
            unsafe
            {
                var ptr1 = (float*)Unsafe.AsPointer(ref span1[0]);
                var ptr2 = (float*)Unsafe.AsPointer(ref span2[0]);
                for (i = 0; i < cnt; i += vectCnt)
                {
                    var v1 = Avx.LoadVector256(ptr1);
                    var v2 = Avx.LoadVector256(ptr2);
                    var t = Avx.Multiply(v1, v2);
                    v3 = Avx.Add(v3, t);
                    ptr1 += vectCnt;
                    ptr2 += vectCnt;
#if DEBUG
                    writer.WriteLine($"{v1.ToString()}\t{v2.ToString()}\t{v3.ToString()}");
#endif
                }
            }

            var rCount = cnt % Vector256<float>.Count;
            var rest1 = span1[^rCount..];
            var rest2 = span2[^rCount..];
            var total = 0f;

            for (i = 0; i < Vector256<float>.Count; i++)
            {
                total += v3.GetElement(i);
            }

            for (i = 0; i < rCount; i++)
            {
                total += rest1[i] * rest2[i];
            }

            if (vector1.Length != vector2.Length)
            {
                Span<float> h, l;
                if (vector1.Length > vector2.Length)
                {
                    h = span1;
                    l = span2;
                }
                else
                {
                    h = span2;
                    l = span1;
                }

                var rest3 = h[l.Length..];
                foreach (var t in rest3)
                {
                    total += t;
                }
            }

            return total;
        }

        public static float DotMultiplyClassic(Memory<float> vector1, Memory<float> vector2)
        {
            var span1 = vector1.Span;
            var span2 = vector2.Span;
            var (hv, lv) = span1.Length > span2.Length ? (vector1, vector2) : (vector2, vector1);
            var hs = hv.Span;
            var ls = lv.Span;
            var total = 0f;
            var i = 0;
#if DEBUG
            var file = Path.GetTempFileName();
            using var writer = new StreamWriter(file);
            Console.WriteLine($"Classic Multiplication will be written in {file}");
#endif
            for (; i < ls.Length; i++)
            {
                total += hs[i] * ls[i];
#if DEBUG
                writer.WriteLine($"{hs[i]}\t{ls[i]}\t{total}");
#endif
            }

            for (; i < hs.Length; i++)
            {
                total += hs[i];
            }

            return total;
        }

        public static float DotMultiplyClassicGroup4(Memory<float> vector1, Memory<float> vector2)
        {
            const int grp = 4;
            var span1 = vector1.Span;
            var span2 = vector2.Span;
            var (hv, lv) = span1.Length > span2.Length ? (vector1, vector2) : (vector2, vector1);
            var hs = hv.Span;
            var ls = lv.Span;
            var total = 0f;
            var i = 0;
#if DEBUG
            var file = Path.GetTempFileName();
            using var writer = new StreamWriter(file);
            Console.WriteLine($"Classic Multiplication Group 4 will be written in {file}");
#endif
            for (; i < ls.Length; i += grp)
            {
                total += hs[i] * ls[i] + hs[i + 1] * ls[i + 1] + hs[i + 2] * ls[i + 2] + hs[i + 3] * ls[i + 3];
#if DEBUG
                writer.WriteLine($"{hs[i]}\t{ls[i]}\t{total}");
#endif
            }

            if (ls.Length % grp != 0)
            {
                for (i += grp; i < ls.Length; i++)
                {
                    total += hs[i] * ls[i];
                }
            }

            for (; i < hs.Length; i++)
            {
                total += hs[i];
            }

            return total;
        }


        public static float DotMultiplyClassicGroup8(Memory<float> vector1, Memory<float> vector2)
        {
            const int grp = 8;
            var span1 = vector1.Span;
            var span2 = vector2.Span;
            var (hv, lv) = span1.Length > span2.Length ? (vector1, vector2) : (vector2, vector1);
            var hs = hv.Span;
            var ls = lv.Span;
            var total = 0f;
            var i = 0;
#if DEBUG
            var file = Path.GetTempFileName();
            using var writer = new StreamWriter(file);
            Console.WriteLine($"Classic Multiplication Group 8 will be written in {file}");
#endif
            for (; i < ls.Length; i += grp)
            {
                total += hs[i] * ls[i] + hs[i + 1] * ls[i + 1] + hs[i + 2] * ls[i + 2] + hs[i + 3] * ls[i + 3]
                         + hs[i + 4] * ls[i + 4] + hs[i + 5] * ls[i + 5] + hs[i + 6] * ls[i + 6] + hs[i + 7] * ls[i + 7];
#if DEBUG
                writer.WriteLine($"{hs[i]}\t{ls[i]}\t{total}");
#endif
            }

            if (ls.Length % grp != 0)
            {
                for (i += grp; i < ls.Length; i++)
                {
                    total += hs[i] * ls[i];
                }
            }

            for (; i < hs.Length; i++)
            {
                total += hs[i];
            }

            return total;
        }
    }
}
