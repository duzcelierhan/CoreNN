using CoreNN;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreNNTest
{
    [TestFixture]
    public class NetworkTests
    {
        [Test]
        [TestCaseSource(nameof(TestPrepareBufferCaseInput))]
        public void TestPrepareBuffer(int inputCount, int[] hiddenLayerNeuronCounts, int outputCount, int expectedBufferLength)
        {
            var hiddenLayerSettings = hiddenLayerNeuronCounts.Select(n => new LayerSettings { NeuronCount = n }).ToArray();

            var buffer = Network.PrepareMemoryBuffer(inputCount, hiddenLayerSettings, new LayerSettings { NeuronCount = outputCount });

            Console.WriteLine($"Created buffer has {buffer.Length} elements");

            Assert.That(buffer, Has.Length.EqualTo(expectedBufferLength));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void TestPrepareBufferThrowsExceptionForNeuronCount(int neuronCount)
        {
            var hiddenLayerSettings = new LayerSettings[] { new LayerSettings { NeuronCount = neuronCount } };
            var outputSettings = new LayerSettings { NeuronCount = 2 };

            Assert.That(() => Network.PrepareMemoryBuffer(1, hiddenLayerSettings, outputSettings),
                Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        private static IEnumerable<TestCaseData> TestPrepareBufferCaseInput()
        {
            /*
             var expectedBufferLength = inputCount + (inputCount * hiddenLayerNeuronCount + hiddenLayerNeuronCount) + hiddenLayerNeuronCount
                + (hiddenLayerNeuronCount * outputCount + outputCount) + outputCount;
            */

            yield return new TestCaseData(1, new[] { 3 }, 2, 20);
            yield return new TestCaseData(1, Array.Empty<int>(), 2, 7);
            //yield return new TestCaseData(1, new[] {0}, 2)
        }
    }
}
