namespace Domain.DTOs.Users.Request
{
    public class UpdateUserRequest
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
