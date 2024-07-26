using System.Runtime.Serialization;

namespace RealEstateAPI.Exceptions
{
    public class NoUserException : Exception
    {
        string msg;
        public NoUserException()
        {
            msg = string.Empty;
        }

        public NoUserException(string? message) : base(message)
        {
            msg = $"{message}";
        }

        public NoUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string Message => msg;
    }
}
