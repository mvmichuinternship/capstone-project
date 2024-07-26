using System.Runtime.Serialization;

namespace RealEstateAPI.Exceptions
{
    public class NoPropertyException: Exception
    {
        string msg;
        public NoPropertyException()
        {
            msg = string.Empty;
        }

        public NoPropertyException(string? message) : base(message)
        {
            msg = $"{message}";
        }
        public override string Message => msg;
    }
}
