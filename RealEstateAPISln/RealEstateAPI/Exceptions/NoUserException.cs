﻿using System.Runtime.Serialization;

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

        public override string Message => msg;
    }
}
