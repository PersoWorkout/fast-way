using Domain.Abstractions;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Models
{
    public class User: BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public EmailValueObject Email { get; set; }
        public PasswordValueObject Password { get; set; }
        public UserRoles Role {  get; set; } = UserRoles.Client;

        public void Update(
            string? firstname = null,
            string? lastname = null, 
            EmailValueObject? email = null, 
            PasswordValueObject? password = null)
        {
            if (!string.IsNullOrEmpty(firstname)) Firstname = firstname;
            if (!string.IsNullOrEmpty(lastname)) Lastname = lastname;
            if (email != null) Email = email;
            if (password != null) Password = password;
            UpdatedAt = DateTime.Now;
        }
    }
}
