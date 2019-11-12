using System;
using System.Collections.Generic;
using System.Linq;
using CoreNN;
using CoreNN.ActivationFunctions;
using CoreNN.Tools;
using NUnit.Framework;

#nullable enable

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

        [TestCaseSource(nameof(SingleLayerTestCaseGenerator))]
        public void SingleLayerTest(int neuronCount, float[] inputs, float[] weightsAndBiases, IActivationFunction activationFunction)
        {
            var inCnt = inputs.Length;
            var outputs = new float[neuronCount];
            var memIn = inputs.AsMemory();
            var weMemFlat = weightsAndBiases.AsMemory();
            var memOut = outputs.AsMemory();
            var weMem = HelpersMisc.SliceArray(ref weMemFlat, inCnt+1, neuronCount);
            var layer = new Layer(neuronCount, ref weMem, ref memIn, ref memOut, activationFunction);
            layer.CalculateWithoutNeuronParallel();

            Assert.Multiple(() =>
            {
                for (var i = 0; i < neuronCount; i++)
                {
                    var wAndB = weMem[i].ToArray();
                    var total = inputs.Select((t, j) => wAndB[j] * t).Sum();

                    total += wAndB[inputs.Length];
                    var nOut = activationFunction.Forward(ref total);
                    Assert.That(outputs[i], Is.EqualTo(nOut)/*.Within(.0001f)*/, $"Output is not as expected on neuron #{i}");
                }
            });
        }

        private static IEnumerable<TestCaseData> SingleLayerTestCaseGenerator()
        {
            //var weights = new[] {.7f, .86f, .11f, .05f, .18f, .35f, .27f, .56f, .79f, .43f};
            var weightsAndBiases = new[]
            {
                .428f, .656f, .974f, .231f, .197f, .489f, .088f, .450f, .220f, .520f,
                .987f, .564f, .172f, .975f, .276f, .623f, .928f, .687f, .614f, .607f,
                .134f, .325f, .841f, .867f, .489f, .262f, .294f, .021f, .723f, .498f,
                .521f, .415f, .351f, .195f, .634f, .072f, .944f, .724f, .616f, .197f,
                .238f, .163f, .270f, .737f, .602f, .289f, .213f, .703f, .998f, .402f,
                .982f, .135f, .448f, .812f, .468f, .103f, .815f, .106f, .741f, .935f,
                .195f, .067f, .727f, .722f, .999f, .572f, .213f, .977f, .341f, .204f,
                .679f, .204f, .071f, .417f, .369f, .972f, .055f, .104f, .265f, .200f,
                .049f, .591f, .023f, .322f, .516f, .386f, .967f, .538f, .727f, .531f,
                .258f, .838f, .982f, .681f, .002f, .171f, .436f, .547f, .738f, .030f,
                .076f, .237f, .362f, .795f, .496f, .853f, .678f, .274f, .025f, .910f,
            };
            var inputs = new[] {.66f, .83f, .12f, .14f, .3f, .48f, .34f, .52f, .65f, .14f};
            var actFunctions = new IActivationFunction[] {new Linear(), new ExpLU(.5f), new ReLU(), new Sigmoid(), new Tanh()};
            foreach (var actFunc in actFunctions)
            {
                for (var inpCnt = 1; inpCnt <= 10; inpCnt++)
                {
                    for (var neuCnt = 1; neuCnt <= 10; neuCnt++)
                    {
                        var ins = inputs[..inpCnt];
                        var wsAndBs = weightsAndBiases[..((inpCnt + 1) * neuCnt)];
                        yield return new TestCaseData(neuCnt, ins, wsAndBs, actFunc).SetName($"SingleLayer - {inpCnt} Inputs - {neuCnt} Neurons - {actFunc.Name}");
                    }
                }
            }
        }
    }
}
