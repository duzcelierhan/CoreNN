using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace CoreNN
{
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    public class Neuron
    {
        [SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "<Pending>")]
        public Memory<float> WeightsAndBias;
        private Memory<float> m_Weights;
        private Memory<float> m_Bias;

        public Neuron(Memory<float> weightsAndBias)
        {
            WeightsAndBias = weightsAndBias;
            m_Weights = WeightsAndBias[..^2];
            m_Bias = WeightsAndBias[^1..];
        }

        public float Calculate(Memory<float> inputs)
        {
            var wv = new Vector<float>(m_Weights.Span);
            var iv = new Vector<float>(inputs.Span);
            var m = Vector.Dot(iv, wv);
            return m + m_Bias.Span[0];
        }
    }
}
