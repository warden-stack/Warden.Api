using System;

namespace Warden.Api.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException()
        {
        }

        public DomainException(string message, params object[] args) : this(null, message, args)
        {
        }

        public DomainException(Exception innerException, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
        }
    }
}