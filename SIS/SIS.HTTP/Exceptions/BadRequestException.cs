namespace SIS.HTTP.Exceptions
{
    using System;

    public class BadRequestException : Exception
    {
        private const string BadRequestExceptionDefaultMessage =
            "The Request was malformed or contains unsupported elements.";
        public BadRequestException()
        : base(message: BadRequestExceptionDefaultMessage)
        {
        }

        public BadRequestException(string message)
        : base(message)
        {
        }
    }
}