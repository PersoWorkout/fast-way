namespace Domain.Models
{
    public class Session(Guid userId, string token)
    {
        public Guid UserId { get; set; } = userId;
        public string Token { get; set; } = token;
        public DateTime ExpiredAt { get; set; } = DateTime.Now.AddHours(1);
    }
}
