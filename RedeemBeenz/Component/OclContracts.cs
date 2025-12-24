using System;

namespace RedeemBeenz.Component
{
    public static class OclContracts
    {
        public static void Pre(bool condition, string message)
        {
            if (!condition) throw new InvalidOperationException("OCL PRE violated: " + message);
        }

        public static void Post(bool condition, string message)
        {
            if (!condition) throw new InvalidOperationException("OCL POST violated: " + message);
        }

        public static void Inv(bool condition, string message)
        {
            if (!condition) throw new InvalidOperationException("OCL INV violated: " + message);
        }
    }
}