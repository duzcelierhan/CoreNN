using System;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace CoreNN.Tools
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable", Justification = "<Pending>")]
    public class Multipliers
    {
        public delegate float dMultWRet(ref Memory<float> vector1, ref Memory<float> vector2);

        public static float DotMultiplyIntrinsicWFma(ref Memory<float> mem1, ref Memory<float> mem2)
        {
            var span1 = mem1.Span;
            var span2 = mem2.Span;
            var cnt = Math.Min(span1.Length, span2.Length);
            var v3 = Vector256.CreateScalarUnsafe(0f);
            var vectLen = Vector256<float>.Count;
            var vectCnt = cnt / vectLen;
#if TEST
            var file = Path.GetTempFileName();
            using var writer = new StreamWriter(file);
            Console.WriteLine($"Intrinsic with Fma Mult. results will be written into {file}");
#endif

            int i;
            unsafe
            {
                for (i = 0; i < vectCnt; i++)
                {
                    var index = i * vectLen;
                    var v1 = Avx.LoadVector256((float*) Unsafe.AsPointer(ref span1[index]));
                    var v2 = Avx.LoadVector256((float*) Unsafe.AsPointer(ref span2[index]));
                    v3 = Fma.MultiplyAdd(v1, v2, v3);
#if TEST
                    writer.WriteLine($"{v1.ToString()}\t{v2.ToString()}\t{v3.ToString()}");
#endif
                }
            }

            var total = 0f;
            for (i = 0; i < vectLen; i++)
            {
                total += v3.GetElement(i);
            }

            for (i = vectCnt * vectLen; i < cnt; i++)
            {
                total += span1[i] * span2[i];
            }

            if (span1.Length != span2.Length)
            {
                var h = span1.Length > span2.Length ? span1 : span2;
                  for (var j = cnt; j < h.Length; j++)
                {
                      total += h[j];
                }
            }

            return total;
        }

        public static float DotMultiplyIntrinsicWFmaWSpanPtr(ref Memory<float> vector1, ref Memory<float> vector2)
        {
            var span1 = vector1.Span;
            var span2 = vector2.Span;
            var cnt = Math.Min(span1.Length, span2.Length);
            var v3 = Vector256.CreateScalarUnsafe(0f);
            var vectLen = Vector256<float>.Count;
            var vectCnt = cnt / vectLen;
            var total = 0f;
#if TEST
            var file = Path.GetTempFileName();
            using var writer = new StreamWriter(file);
            Console.WriteLine($"Intrinsic with FmaWPtr Mult. results will be written into {file}");
#endif

            unsafe
            {
                int i;
                var ptr1 = (float*) Unsafe.AsPointer(ref span1[0]);
                var ptr2 = (float*) Unsafe.AsPointer(ref span2[0]);

                for (i = 0; i < vectCnt; i++)
                {
                    var v1 = Avx.LoadVector256(ptr1);
                    var v2 = Avx.LoadVector256(ptr2);
                    v3 = Fma.MultiplyAdd(v1, v2, v3);
                    ptr1 += vectLen;
                    ptr2 += vectLen;
#if TEST
                    writer.WriteLine($"{v1.ToString()}\t{v2.ToString()}\t{v3.ToString()}");
#endif
                }

                for (i = 0; i < vectLen; i++)
                {
                    total += v3.GetElement(i);
                }

                i = vectCnt * vectLen;
                if (cnt % vectLen > 0)
                {
                    ptr1 = (float*) Unsafe.AsPointer(ref span1[i]);
                    ptr2 = (float*) Unsafe.AsPointer(ref span2[i]);
                    for (; i < cnt; i++)
                    {
                        total += *ptr1++ * *ptr2++;
                    }
                }
            }

            if (vector1.Length != vector2.Length)
            {
                var h = vector1.Length > vector2.Length ? span1 : span2;
                  for (var j = cnt; j < h.Length; j++)
                {
                    total += h[j];
                }
            }

            return total;
        }

        public static float DotMultiplyIntrinsicWAvx(ref Memory<float> vector1, ref Memory<float> vector2)
        {
            var span1 = vector1.Span;
            var span2 = vector2.Span;
            var cnt = Math.Min(span1.Length, span2.Length);
            var v3 = Vector256.CreateScalarUnsafe(0f);
            var vectLen = Vector256<float>.Count;
            var vectCnt = cnt / vectLen;
#if TEST
            var file = Path.GetTempFileName();
            using var writer = new StreamWriter(file);
            Console.WriteLine($"Intrinsic with Avx Mult. results will be written into {file}");
#endif
            int i;
            unsafe
            {
                for (i = 0; i < vectCnt; i++)
                {
                    var index = i * vectLen;
                    var v1 = Avx.LoadVector256((float*)Unsafe.AsPointer(ref span1[index]));
                    var v2 = Avx.LoadVector256((float*)Unsafe.AsPointer(ref span2[index]));
                    var t = Avx.Multiply(v1, v2);
                    v3 = Avx.Add(v3, t);
#if TEST
                    writer.WriteLine($"{v1.ToString()}\t{v2.ToString()}\t{v3.ToString()}");
#endif
                }
            }

            var total = 0f;
            for (i = 0; i < vectLen; i++)
            {
                total += v3.GetElement(i);
            }

            for (i = vectCnt * vectLen; i < cnt; i++)
            {
                total += span1[i] * span2[i];
            }

            if (vector1.Length != vector2.Length)
            {
                var h = vector1.Length > vector2.Length ? span1 : span2;
                  for (var j = cnt; j < h.Length; j++)
                {
                    total += h[j];
                }
            }

            return total;
        }

        public static float DotMultiplyIntrinsicWAvxWSpanPtr(ref Memory<float> vector1, ref Memory<float> vector2)
        {
            var span1 = vector1.Span;
            var span2 = vector2.Span;
            var cnt = Math.Min(span1.Length, span2.Length);
            var v3 = Vector256.CreateScalarUnsafe(0f);
            var vectLen = Vector256<float>.Count;
            var vectCnt = cnt / vectLen;
            var total = 0f;
#if TEST
            var file = Path.GetTempFileName();
            using var writer = new StreamWriter(file);
            Console.WriteLine($"Intrinsic with AvxWPtr Mult. results will be written into {file}");
#endif
            int i;
            unsafe
            {
                var ptr1 = (float*)Unsafe.AsPointer(ref span1[0]);
                var ptr2 = (float*)Unsafe.AsPointer(ref span2[0]);
                for (i = 0; i < vectCnt; i++)
                {
                    var v1 = Avx.LoadVector256(ptr1);
                    var v2 = Avx.LoadVector256(ptr2);
                    var t = Avx.Multiply(v1, v2);
                    v3 = Avx.Add(v3, t);
                    ptr1 += vectLen;
                    ptr2 += vectLen;
#if TEST
                    writer.WriteLine($"{v1.ToString()}\t{v2.ToString()}\t{v3.ToString()}");
#endif
                }

                for (i = 0; i < vectLen; i++)
                {
                    total += v3.GetElement(i);
                }

                i = vectCnt * vectLen;
                if (cnt % vectLen > 0)
                {
                    ptr1 = (float*)Unsafe.AsPointer(ref span1[i]);
                    ptr2 = (float*)Unsafe.AsPointer(ref span2[i]);
                    for (; i < cnt; i++)
                    {
                        total += *ptr1++ * *ptr2++;
                    }
                }
            }

            if (vector1.Length != vector2.Length)
            {
                var h = vector1.Length > vector2.Length ? span1 : span2;
                  for (var j = cnt; j < h.Length; j++)
                {
                    total += h[j];
                }
            }

            return total;
        }

        public static float DotMultiplyIntrinsicWVector(ref Memory<float> mem1, ref Memory<float> mem2)
        {
            var span1 = mem1.Span;
            var span2 = mem2.Span;
            var cnt = Math.Min(span1.Length, span2.Length);
            var v3 = Vector<float>.Zero;
            var vectLen = Vector<float>.Count;
            var vectCnt = cnt / vectLen;
#if TEST
            var file = Path.GetTempFileName();
            using var writer = new StreamWriter(file);
            Console.WriteLine($"Intrinsic with Fma Mult. results will be written into {file}");
#endif

            int i;
            var total = 0f;
            for (i = 0; i < vectCnt; i++)
            {
                var index = i * vectLen;
                var v1 = new Vector<float>(span1.Slice(index));
                var v2 = new Vector<float>(span2.Slice(index));
                v3 += v1 * v2;
#if TEST
                    writer.WriteLine($"{v1.ToString()}\t{v2.ToString()}\t{v3.ToString()}");
#endif
            }

            for (i = 0; i < vectLen; i++)
            {
                total += v3[i];
            }

            for (i = vectCnt * vectLen; i < cnt; i++)
            {
                total += span1[i] * span2[i];
            }

            if (span1.Length != span2.Length)
            {
                var h = span1.Length > span2.Length ? span1 : span2;
                for (var j = cnt; j < h.Length; j++)
                {
                    total += h[j];
                }
            }

            return total;
        }

        public static float DotMultiplyIntrinsicWVectorDot(ref Memory<float> mem1, ref Memory<float> mem2)
        {
            var span1 = mem1.Span;
            var span2 = mem2.Span;
            var cnt = Math.Min(span1.Length, span2.Length);
            var vectLen = Vector<float>.Count;
            var vectCnt = cnt / vectLen;
#if TEST
            var file = Path.GetTempFileName();
            using var writer = new StreamWriter(file);
            Console.WriteLine($"Intrinsic with Fma Mult. results will be written into {file}");
#endif

            int i;
            var total = 0f;
            for (i = 0; i < vectCnt; i++)
            {
                var index = i * vectLen;
                var v1 = new Vector<float>(span1.Slice(index));
                var v2 = new Vector<float>(span2.Slice(index));
                total += Vector.Dot(v1, v2);
#if TEST
                    writer.WriteLine($"{v1.ToString()}\t{v2.ToString()}\t{v3.ToString()}");
#endif
            }

            for (i = vectCnt * vectLen; i < cnt; i++)
            {
                total += span1[i] * span2[i];
            }

            if (span1.Length != span2.Length)
            {
                var h = span1.Length > span2.Length ? span1 : span2;
                for (var j = cnt; j < h.Length; j++)
                {
                    total += h[j];
                }
            }

            return total;
        }

        public static float DotMultiplyIntrinsicWVectorMul(ref Memory<float> mem1, ref Memory<float> mem2)
        {
            var span1 = mem1.Span;
            var span2 = mem2.Span;
            var cnt = Math.Min(span1.Length, span2.Length);
            var v3 = Vector<float>.Zero;
            var vectLen = Vector<float>.Count;
            var vectCnt = cnt / vectLen;
#if TEST
            var file = Path.GetTempFileName();
            using var writer = new StreamWriter(file);
            Console.WriteLine($"Intrinsic with Fma Mult. results will be written into {file}");
#endif

            int i;
            var total = 0f;
            for (i = 0; i < vectCnt; i++)
            {
                var index = i * vectLen;
                var v1 = new Vector<float>(span1.Slice(index));
                var v2 = new Vector<float>(span2.Slice(index));
                var t = Vector.Multiply(v1, v2);
                v3 = Vector.Add(v3, t);
#if TEST
                    writer.WriteLine($"{v1.ToString()}\t{v2.ToString()}\t{v3.ToString()}");
#endif
            }

            for (i = 0; i < vectLen; i++)
            {
                total += v3[i];
            }

            for (i = vectCnt * vectLen; i < cnt; i++)
            {
                total += span1[i] * span2[i];
            }

            if (span1.Length != span2.Length)
            {
                var h = span1.Length > span2.Length ? span1 : span2;
                for (var j = cnt; j < h.Length; j++)
                {
                    total += h[j];
                }
            }

            return total;
        }

        public static float DotMultiplyClassic(ref Memory<float> vector1, ref Memory<float> vector2)
        {
            var (hv, lv) = vector1.Length > vector2.Length ? (vector1, vector2) : (vector2, vector1);
            var hs = hv.Span;
            var ls = lv.Span;
            var total = 0f;
            var i = 0;
#if TEST
            var file = Path.GetTempFileName();
            using var writer = new StreamWriter(file);
            Console.WriteLine($"Classic Multiplication will be written in {file}");
#endif
            for (; i < ls.Length; i++)
            {
                total += hs[i] * ls[i];
#if TEST
                writer.WriteLine($"{hs[i]}\t{ls[i]}\t{total}");
#endif
            }

            for (; i < hs.Length; i++)
            {
                total += hs[i];
            }

            return total;
        }

        public static float DotMultiplyClassicWPtr(ref Memory<float> vector1, ref Memory<float> vector2)
        {
            var (hv, lv) = vector1.Length > vector2.Length ? (vector1, vector2) : (vector2, vector1);
            var hl = hv.Length;
            var ll = lv.Length;
            var total = 0f;
            unsafe
            {
                  var hs = (float*) Unsafe.AsPointer(ref hv.Span[0]);
                var ls = (float*) Unsafe.AsPointer(ref lv.Span[0]);
                var i = 0;
#if TEST
            var file = Path.GetTempFileName();
            using var writer = new StreamWriter(file);
            Console.WriteLine($"Classic Multiplication will be written in {file}");
#endif
                for (; i < ll; i++)
                {
                    total += *hs++ * *ls++;
#if TEST
                writer.WriteLine($"{hs[i]}\t{ls[i]}\t{total}");
#endif
                }

                for (; i < hl; i++)
                {
                    total += *hs++;
                }
            }

            return total;
        }

        public static float DotMultiplyClassicGroup4(ref Memory<float> vector1, ref Memory<float> vector2)
        {
            const int grp = 4;
            var (hv, lv) = vector1.Length > vector2.Length ? (vector1, vector2) : (vector2, vector1);
            var hs = hv.Span;
            var ls = lv.Span;
            var total = 0f;
            var i = 0;
#if TEST
            var file = Path.GetTempFileName();
            using var writer = new StreamWriter(file);
            Console.WriteLine($"Classic Multiplication Group 4 will be written in {file}");
#endif
            for (; i < ls.Length; i += grp)
            {
                total += hs[i] * ls[i] + hs[i + 1] * ls[i + 1] + hs[i + 2] * ls[i + 2] + hs[i + 3] * ls[i + 3];
#if TEST
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

        public static float DotMultiplyClassicGroup8(ref Memory<float> vector1, ref Memory<float> vector2)
        {
            const int grp = 8;
            var (hv, lv) = vector1.Length > vector2.Length ? (vector1, vector2) : (vector2, vector1);
            var hs = hv.Span;
            var ls = lv.Span;
            var total = 0f;
            var i = 0;
#if TEST
            var file = Path.GetTempFileName();
            using var writer = new StreamWriter(file);
            Console.WriteLine($"Classic Multiplication Group 8 will be written in {file}");
#endif
            for (; i < ls.Length; i += grp)
            {
                total += hs[i] * ls[i] + hs[i + 1] * ls[i + 1] + hs[i + 2] * ls[i + 2] + hs[i + 3] * ls[i + 3]
                         + hs[i + 4] * ls[i + 4] + hs[i + 5] * ls[i + 5] + hs[i + 6] * ls[i + 6] + hs[i + 7] * ls[i + 7];
#if TEST
                writer.WriteLine($"{hs[i]}\t{ls[i]}\t{total}");
#endif
            }

            if (ls.Length % grp != 0)
            {
                var len = ls.Length;
                for (i += grp; i < len; i++)
                {
                    total += hs[i] * ls[i];
                }
            }

            var ln = hs.Length;
            for (; i < ln; i++)
            {
                total += hs[i];
            }

            return total;
        }
    }
}
