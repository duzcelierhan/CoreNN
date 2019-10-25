using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using CoreNN.ActivationFunctions;
using CoreNN.Tools;

namespace CoreNN
{
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    public class Neuron
    {
        private static readonly Multipliers.dMultWRet Multiplier;

        static Neuron()
        {
            if (Fma.IsSupported) Multiplier = Multipliers.DotMultiplyIntrinsicWFmaWSpanPtr;
            else if (Avx.IsSupported) Multiplier = Multipliers.DotMultiplyIntrinsicWAvxWSpanPtr;
            else Multiplier = Multipliers.DotMultiplyClassicGroup8;
        }

        [SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "<Pending>")]
        public Memory<float> WeightsAndBias;
        private Memory<float> m_Weights;
        private Memory<float> m_Bias;
        private Memory<float> m_Inputs;
        private readonly IActivationFunction m_ActivationFunction;
        private Memory<float> m_Output;

        public Neuron(Memory<float> weightsAndBias, Memory<float> inputs, Memory<float> output, IActivationFunction activationFunction)
        {
            WeightsAndBias = weightsAndBias;
            m_Weights = WeightsAndBias[..^1];
            m_Bias = WeightsAndBias[^1..];
            m_Inputs = inputs;
            m_Output = output;
            m_ActivationFunction = activationFunction;
        }

        public float Calculate(Memory<float> inputs)
        {
            var wv = new Vector<float>(m_Weights.Span);
            var iv = new Vector<float>(inputs.Span);
            var m = Vector.Dot(iv, wv);
            return m + m_Bias.Span[0];
        }

        public void Calculate(ref float output)
        {
            
        }
    }
}
