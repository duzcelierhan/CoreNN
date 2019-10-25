using System;
using CoreNN;
using CoreNN.ActivationFunctions;
using CoreNN.Tools;
using NUnit.Framework;

namespace CoreNNTest
{
    public class LayerTests
    {
        [Test]
        public void SingleLayerSingleNeuron()
        {
            var inputs = new[] {.5f};
            var weightsAndBiases = new[]{.5f, 1f};
            var outputs = new[] {0f};
            var actFunc = new Linear();
            var memIn = inputs.AsMemory();
            var weMemFlat = weightsAndBiases.AsMemory();
            var memOut = outputs.AsMemory();
            var weMem = HelpersMisc.SliceArray(ref weMemFlat, 2, 1);
            var layer = new Layer(1, ref weMem, ref memIn, ref memOut, actFunc);
            layer.CalculateWithoutNeuronParallel();

            Assert.That(outputs[0], Is.EqualTo(.5f * .5f + 1f));
        }

        [Test]
        public void SingleLayerTwoNeurons()
        {
            var inputs = new[] { .5f };
            var weightsAndBiases = new[] { .5f, .1f, .25f, .2f };
            var outputs = new[] { 0f, 0f };
            var actFunc = new Linear();
            var memIn = inputs.AsMemory();
            var weMemFlat = weightsAndBiases.AsMemory();
            var memOut = outputs.AsMemory();
            var weMem = HelpersMisc.SliceArray(ref weMemFlat, 2, 2);
            var layer = new Layer(2, ref weMem, ref memIn, ref memOut, actFunc);
            layer.CalculateWithoutNeuronParallel();

            Assert.That(outputs[0], Is.EqualTo(.5f * .5f + .1f));
            Assert.That(outputs[1], Is.EqualTo(.5f * .25f + .2f));
        }
    }
}
