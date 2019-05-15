namespace SIS.HTTP.Exceptions
{
    using System;

    public class InternalServerErrorException : Exception
    {
        private const string InternalServerErrorExceptionDefaultMessage = "The Server has encountered an error.";
        public InternalServerErrorException()
        : base(InternalServerErrorExceptionDefaultMessage)
        {
        }

        public InternalServerErrorException(string message)
        : base(message)
        {

        }
    }
}