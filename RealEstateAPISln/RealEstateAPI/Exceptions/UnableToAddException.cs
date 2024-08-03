using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RealEstateAPI.Exceptions
{
    [ExcludeFromCodeCoverage]
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

        public override string Message => msg;
    }
}
