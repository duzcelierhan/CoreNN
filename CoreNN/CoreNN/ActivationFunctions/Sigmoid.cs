using System;
using System.Runtime.CompilerServices;

namespace CoreNN.ActivationFunctions
{
    public class Sigmoid : IActivationFunction
    {
        #region Implementation of IActivationFunction

        /// <inheritdoc />
        public string Name => nameof(Sigmoid);

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Forward(ref float value) => 1.0f / (1.0f + MathF.Exp(value));

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Reverse(ref float value)
        {
            var f = Forward(ref value);
            return f * (1.0f - f);
        }

        #endregion
    }
}
