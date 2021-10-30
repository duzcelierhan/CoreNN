using CoreNN.ActivationFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreNN
{
    public class Network
    {
        public int InputCount { get; }
        public Layer Input { get; private set; }
        public Layer[] HiddenLayers { get; private set; }
        public Layer Output { get; private set; }

        public Network(int inputCount, LayerSettings[] hiddenLayerSettings, LayerSettings outputLayerSettings)
        {
            InputCount = inputCount;

            if(hiddenLayerSettings is not null)
            {
                int layerInputCount = inputCount;

            }
        }

        internal static float[] PrepareMemoryBuffer(int inputCount, LayerSettings[] hiddenLayerSettings, LayerSettings outputLayerSettings)
        {
            var totalNumber = 0;

            totalNumber += inputCount; // First, keep buffer to write the network input
            var layerInputCount = inputCount;
            if (hiddenLayerSettings?.Length > 0)
            {
                var hiddenLayerNumber = 0;
                foreach (var setting in hiddenLayerSettings)
                {
                    if (setting.NeuronCount <= 0)
                        throw new ArgumentOutOfRangeException($"Every hidden layer must have non-negative neuron count: " +
                            $"HiddenLayerNumber {hiddenLayerNumber}; NeuronCount: {setting.NeuronCount}");
                    totalNumber += (layerInputCount + 1) * setting.NeuronCount; // For each neuron, we have 1 weight per input + 1 bias
                    totalNumber += setting.NeuronCount; // Outputs of the layer
                    layerInputCount = setting.NeuronCount; // One layer's output is nex one's input
                    hiddenLayerNumber++;
                }
            }
            totalNumber += (layerInputCount + 1) * outputLayerSettings.NeuronCount;
            totalNumber += outputLayerSettings.NeuronCount; // Network's outputs

            return new float[totalNumber];
        }
    }

    public struct LayerSettings
    {
        public int NeuronCount { get; set; }
        public IActivationFunction ActivationFunction { get; set; }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NeuronCount, ActivationFunction);
        }

        public static bool operator ==(LayerSettings left, LayerSettings right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LayerSettings left, LayerSettings right)
        {
            return !(left == right);
        }
    }
}
