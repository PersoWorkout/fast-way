using Domain.Abstractions;

namespace Domain.Models
{
    public class User: BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
    }
}
