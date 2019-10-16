using System;
using System.Runtime.CompilerServices;

namespace CoreNN.ActivationFunctions
{
    /// <summary>
    /// Rectified Linear Unit Activation Function
    /// </summary>
    public class ReLU : IActivationFunction
    {
        #region Implementation of IActivationFunction

        /// <inheritdoc />
        public string Name => nameof(ReLU);

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Forward(ref float value) => MathF.Max(0.0f, value);

        // TODO: Check the performance of MathF.CopySign and conditional return statement
        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Reverse(ref float value) => MathF.CopySign(1.0f, value);

        #endregion
    }
}
