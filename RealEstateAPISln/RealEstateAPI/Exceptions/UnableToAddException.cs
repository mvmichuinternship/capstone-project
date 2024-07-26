using System.Runtime.Serialization;

namespace RealEstateAPI.Exceptions
{
    public class UnableToAddException : Exception
    {
        string msg;
        public UnableToAddException()
        {
            msg = string.Empty;
        }

        public UnableToAddException(string? message) : base(message)
        {
            msg = $"{message}";
        }

        public UnableToAddException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToAddException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string Message => msg;
    }
}
