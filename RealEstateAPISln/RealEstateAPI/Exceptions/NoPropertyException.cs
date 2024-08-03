using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RealEstateAPI.Exceptions
{
    [ExcludeFromCodeCoverage]
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
