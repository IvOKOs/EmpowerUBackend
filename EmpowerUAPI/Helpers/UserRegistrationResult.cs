namespace EmpowerUAPI.Helpers
{
    public class UserRegistrationResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }

        private UserRegistrationResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static UserRegistrationResult Success()
        {
            return new UserRegistrationResult(true, string.Empty);
        }

        public static UserRegistrationResult Failure(string message)
        {
            return new UserRegistrationResult(false, message);
        }
    }
}
