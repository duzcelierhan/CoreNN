using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using CoreNN.ActivationFunctions;
using CoreNN.Tools;

#nullable enable

namespace CoreNN
{
    public class Layer //: IInputProvider
    {
        private Memory<float> m_Inputs;
        private Memory<float> m_Outputs;
        private Memory<float>[] m_Weights;
        private Neuron[] m_Neurons;
        private IActivationFunction m_ActivationFunction;

        private static readonly Multipliers.dMultWRet Multiplier;

        static Layer()
        {
            if (Fma.IsSupported) Multiplier = Multipliers.DotMultiplyIntrinsicWFmaWSpanPtr;
            else if (Avx.IsSupported) Multiplier = Multipliers.DotMultiplyIntrinsicWAvxWSpanPtr;
            else Multiplier = Multipliers.DotMultiplyClassicGroup8;
        }

        public Layer(int neuronCount, ref Memory<float>[] weightsAndBiases, ref Memory<float> inputs, ref Memory<float> outputs, IActivationFunction activationFunction)
        {
            if (weightsAndBiases.Length != neuronCount || inputs.Length != weightsAndBiases[0].Length - 1 || outputs.Length != neuronCount)
                throw new InvalidOperationException("Please check the arguments given");
            m_Inputs = inputs;
            m_Outputs = outputs;
            m_ActivationFunction = activationFunction;
            m_Weights = weightsAndBiases;

            m_Neurons = new Neuron[neuronCount];
            for (var i = 0; i < neuronCount; i++)
            {
                m_Neurons[i] = new Neuron(weightsAndBiases[i], inputs, outputs, activationFunction);
            }
        }

        public unsafe void CalculateWithoutNeuronParallel()
        {
            var ptr = (float*) Unsafe.AsPointer(ref MemoryMarshal.GetReference(m_Outputs.Span));

            Parallel.ForEach(m_Weights, Calc);

            void Calc(Memory<float> weights, ParallelLoopState state, long index)
            {
                var ret = Multiplier(ref weights, ref m_Inputs);
                ptr[index] = m_ActivationFunction.Forward(ref ret);
            }
        }

        public void CalculateLayerParallel()
        {
            Parallel.ForEach(m_Neurons, CalculateNeuron);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CalculateNeuron(Neuron neuron)
        {
            neuron.Calculate(m_Inputs);
        }
    }
}
