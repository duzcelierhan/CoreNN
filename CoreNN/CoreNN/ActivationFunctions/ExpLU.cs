using System;

namespace CoreNN.ActivationFunctions
{
    public class ExpLU : IActivationFunction
    {
        private readonly float m_Gradient;

        public ExpLU(float gradient) => m_Gradient = gradient;

        #region Implementation of IActivationFunction

        /// <inheritdoc />
        public string Name => "ELU";

        /// <inheritdoc />
        public float Forward(ref float value) => value > 0.0f ? value : m_Gradient * (MathF.Exp(value) - 1);

        /// <inheritdoc />
        public float Reverse(ref float value) => value > 0.0f ? 1.0f : m_Gradient * MathF.Exp(value);

        #endregion
    }
}
