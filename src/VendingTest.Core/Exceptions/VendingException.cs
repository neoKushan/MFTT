namespace VendingTest.Core.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class VendingException : Exception
    {
        public VendingException(string message)
            : base(message)
        {
        }

        public VendingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected VendingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
