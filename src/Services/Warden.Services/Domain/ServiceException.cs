using System;

namespace Warden.Services.Domain
{
    public class ServiceException : Exception
    {
        public ServiceException()
        {
        }

        public ServiceException(string message, params object[] args) : this(null, message, args)
        {
        }

        public ServiceException(Exception innerException, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
        }
    }
}