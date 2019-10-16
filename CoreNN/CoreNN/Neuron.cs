using System;

namespace CoreNN
{
    public class Neuron
    {
        public Memory<float> Weights;

        public Neuron(Memory<float> weights) => Weights = weights;
    }
}
