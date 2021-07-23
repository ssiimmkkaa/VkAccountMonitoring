using System;

namespace ConsoleApp.Exceptions.AuthorizeExceptions
{
    class AuthorizeException : Exception
    {
        public override string Message => $"Ошибка авторизации";

        public override string ToString()
        {
            return Message;
        }
    }
}