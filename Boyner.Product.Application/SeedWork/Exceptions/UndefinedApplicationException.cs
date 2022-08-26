using System;

namespace Boyner.Product.Application.SeedWork.Exceptions
{
    /// <summary>
    /// Exception base of application layer
    /// </summary>
    public class UndefinedApplicationException : Exception
    {
        public UndefinedApplicationException()
        { }

        public UndefinedApplicationException(string message)
            : base(message)
        { }

        public UndefinedApplicationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
