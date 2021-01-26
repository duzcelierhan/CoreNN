using System;
using BenchmarkDotNet.Attributes;
using static CoreNN.Tools.Multipliers;
using static CoreNNTest.Tests;
// ReSharper disable ArrangeMethodOrOperatorBody

namespace Benchmarker
{
    [RankColumn]
    [MemoryDiagnoser]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.NetCoreApp50)]
    [DisassemblyDiagnoser(printInstructionAddresses: true, printSource: true)]
    public class Benchmarker
    {
        private Memory<float> m_Ma1;
        private Memory<float> m_Ma2;

        public Benchmarker()
        {
            float[] fa2;
            float[] fa1;
            (fa1, fa2) = Read2ColumnFloatsFromFile(F2_1MFile);
            (m_Ma1, m_Ma2) = (fa1.AsMemory(), fa2.AsMemory());
        }

        [Benchmark]
        public float MultiplyAvx()
        {
            return DotMultiplyIntrinsicWAvx(ref m_Ma1, ref m_Ma2);
        }

        [Benchmark]
        public float MultiplyAvxWSpanPtr()
        {
            return DotMultiplyIntrinsicWAvxWSpanPtr(ref m_Ma1, ref m_Ma2);
        }

        [Benchmark]
        public float MultiplyFma()
        {
            return DotMultiplyIntrinsicWFma(ref m_Ma1, ref m_Ma2);
        }

        [Benchmark]
        public float MultiplyFmaWSpanPtr()
        {
            return DotMultiplyIntrinsicWFmaWSpanPtr(ref m_Ma1, ref m_Ma2);
        }

        [Benchmark]
        public float MultiplyWithVector()
        {
            return DotMultiplyIntrinsicWVector(ref m_Ma1, ref m_Ma2);
        }

        [Benchmark]
        public float MultiplyWithVectorDot()
        {
            return DotMultiplyIntrinsicWVectorDot(ref m_Ma1, ref m_Ma2);
        }

        [Benchmark]
        public float MultiplyWithVectorMul()
        {
            return DotMultiplyIntrinsicWVectorMul(ref m_Ma1, ref m_Ma2);
        }

        [Benchmark(Baseline = true)]
        public float MultiplyClassicSingle()
        {
            return DotMultiplyClassic(ref m_Ma1, ref m_Ma2);
        }

        [Benchmark]
        public float MultiplyClassicSingleWPtr()
        {
            return DotMultiplyClassicWPtr(ref m_Ma1, ref m_Ma2);
        }

        [Benchmark]
        public float MultiplyClassicGroup4()
        {
            return DotMultiplyClassicGroup4(ref m_Ma1, ref m_Ma2);
        }

        [Benchmark]
        public float MultiplyClassicGroup8()
        {
            return DotMultiplyClassicGroup8(ref m_Ma1, ref m_Ma2);
        }
    }
}
