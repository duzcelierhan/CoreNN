namespace CoreNN.ActivationFunctions
{
    public class LeakyReLU : IActivationFunction
    {
        private readonly float m_Gradient;

        public LeakyReLU(float gradient) => m_Gradient = gradient;

        #region Implementation of IActivationFunction

        /// <inheritdoc />
        public string Name => nameof(LeakyReLU);

        /// <inheritdoc />
        public float Forward(ref float value) => value > 0.0f ? value : value * m_Gradient;

        /// <inheritdoc />
        public float Reverse(ref float value) => value > 0.0f ? 1.0f : m_Gradient;

        #endregion
    }
}
