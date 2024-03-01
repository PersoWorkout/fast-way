using Domain.Models;
using Domain.ValueObjects;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> Create(User user);
        Task<User?> GetById(Guid id);
        Task<User?> GetByEmail(EmailValueObject email);
        Task<bool> EmailAlreadyUsed(EmailValueObject email);
        Task<User> Update(User user);
        Task<bool> Delete(User user);
    }
}
