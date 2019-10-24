using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using static CoreNN.Tools.Multipliers;
using static CoreNNTest.Tests;

namespace Benchmarker
{
    [RankColumn]
    [MemoryDiagnoser]
    [CoreJob]
    public class Benchmarker
    {
        private readonly float[] fa1;
        private readonly float[] fa2;
        private readonly Memory<float> ma1;
        private readonly Memory<float> ma2;

        public Benchmarker()
        {
            (fa1, fa2) = Read2ColumnFloatsFromFile(F2_1MFile);
            (ma1, ma2) = (fa1.AsMemory(), fa2.AsMemory());
        }

        [Benchmark]
        public float MultiplyAvx()
        {
            return DotMultiplyIntrinsicWAvx(ma1, ma2);
        }

        [Benchmark]
        public float MultiplyAvxWSpanPtr()
        {
            return DotMultiplyIntrinsicWAvxWSpanPtr(ma1, ma2);
        }

        [Benchmark]
        public float MultiplyFma()
        {
            return DotMultiplyIntrinsicWFma(ma1, ma2);
        }

        [Benchmark]
        public float MultiplyFmaWSpanPtr()
        {
            return DotMultiplyIntrinsicWFmaWSpanPtr(ma1, ma2);
        }

        [Benchmark(Baseline = true)]
        public float MultiplyClassicSingle()
        {
            return DotMultiplyClassic(ma1, ma2);
        }

        [Benchmark]
        public float MultiplyClassicGroup4()
        {
            return DotMultiplyClassicGroup4(ma1, ma2);
        }

        [Benchmark]
        public float MultiplyClassicGroup8()
        {
            return DotMultiplyClassicGroup8(ma1, ma2);
        }
    }
}
