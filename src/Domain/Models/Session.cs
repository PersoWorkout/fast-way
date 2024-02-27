namespace Domain.Models
{
    public class Session
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
