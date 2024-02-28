namespace Domain.DTOs.Authorization
{
    public class ConnectedResponse
    {
        public string Token { get; set; }
        public DateTime ExpiredAt {  get; set; }
    }
}
