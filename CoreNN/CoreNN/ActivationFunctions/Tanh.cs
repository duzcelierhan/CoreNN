using System;

namespace CoreNN.ActivationFunctions
{
    public class Tanh : IActivationFunction
    {
        #region Implementation of IActivationFunction

        /// <inheritdoc />
        public string Name => nameof(Tanh);

        /// <inheritdoc />
        public float Forward(ref float value)
        {
            var e1 = MathF.Exp(value);
            var e2 = MathF.Exp(-value);

            return (e1 + e2) / (e1 - e2);
        }

        /// <inheritdoc />
        public float Reverse(ref float value)
        {
            var v = Forward(ref value);
            // return 1.0f - MathF.Pow(v, 2.0f);
            return 1.0f - v * v;
        }

        #endregion
    }
}
