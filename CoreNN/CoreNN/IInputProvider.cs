using System;
using System.Collections.Generic;
using System.Text;

namespace CoreNN
{
    public interface IInputProvider
    {
        Memory<float> ProvideInput();
    }
}
