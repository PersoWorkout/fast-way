using Domain.ValueObjects;

namespace Domain.DTOs.Authorization.Requests
{
    public class RegisterRequest
    {
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PasswordConfirmation { get; set; }
    }
}
