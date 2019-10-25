using System;
using System.Collections.Generic;
using System.Text;

namespace CoreNN.Tools
{
    public class HelpersMisc
    {
        public static Memory<float>[] SliceArray(ref Memory<float> array, int memLen, int arrayLen)
        {
            if (array.Length < memLen * arrayLen)
                throw new ArgumentException("Array cannot be sliced");
            var arr = new Memory<float>[arrayLen];
            for (var i = 0; i < arrayLen; i++)
            {
                arr[i] = array.Slice(i * memLen, memLen);
            }

            return arr;
        }
    }
}
