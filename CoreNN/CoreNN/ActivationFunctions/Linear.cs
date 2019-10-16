namespace CoreNN.ActivationFunctions
{
    public class Linear : IActivationFunction
    {
        #region Implementation of IActivationFunction

        /// <inheritdoc />
        public string Name => nameof(Linear);

        /// <inheritdoc />
        public float Forward(ref float value) => value;

        /// <inheritdoc />
        public float Reverse(ref float value) => 1.0f;

        #endregion
    }
}
