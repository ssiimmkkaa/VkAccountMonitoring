namespace ConsoleApp.Exceptions.AuthorizeExceptions
{
    class UserNotAuthorizedException : AuthorizeException
    {
        private string _userId;

        public UserNotAuthorizedException()
        {
        }

        public UserNotAuthorizedException(string userId)
        {
            _userId = userId;
        }

        public override string Message => $"{base.Message}. Пользователь {_userId} не авторизован";

        public override string ToString()
        {
            return Message;
        }
    }
}