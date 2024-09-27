namespace GMS.Shared.Constants
{
    public class ErrorMessages
    {
        // shared
        public const string Ok = "Your request was processed successfully.";
        public const string BadRequest = "Validation failed. Please check your input data.";
        public const string Unauthorized = "You are unauthorized to access this resource.";
        public const string Forbidden = "You don't have permission to access this resource.";
        public const string NotFound = "The requested resource was not found.";
        public const string Conflict = "Unable to complete the requested operation due to a conflict.";
        public const string InternalServerError = "Server encountered an unexpected error. Please retry later or contact support for assistance.";
        
        // account
        public const string DuplicateEmailAddress = "The email address you entered is already in use. Please use a different email address.";
        public const string InvalidEmailOrPassword = "Invalid email or password. Please try again.";
        public const string InvalidOldPassword = "The old password you entered is incorrect. Please try again.";
        public const string InvalidRefreshToken = "The refresh token is invalid or expired. Please log in again.";
    }
}
