using System.Runtime.Serialization;

namespace RealEstateAPI.Exceptions
{
    public class UnauthorizedUserException : Exception
    {
        string msg;
        public UnauthorizedUserException()
        {
            msg = string.Empty;
        }

        public UnauthorizedUserException(string? message) : base(message)
        {
            msg = $"{message}";
        }

        public UnauthorizedUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnauthorizedUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string Message => msg;
    }
}
