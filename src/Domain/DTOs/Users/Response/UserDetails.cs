namespace Domain.DTOs.Users.Response
{
    public class UserDetails
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
    }
}
