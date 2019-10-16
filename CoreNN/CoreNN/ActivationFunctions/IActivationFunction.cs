using System.Runtime.CompilerServices;

namespace CoreNN.ActivationFunctions
{
    public interface IActivationFunction
    {
        string Name { get; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        float Forward(ref float value);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        float Reverse(ref float value);
    }
}
