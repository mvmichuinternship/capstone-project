using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RealEstateAPI.Exceptions
{
    [ExcludeFromCodeCoverage]
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

        public override string Message => msg;
    }
}
