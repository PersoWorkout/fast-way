using Domain.Models;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> Create(User user);
        Task<User> GetById(Guid id);
        Task<bool> EmailAlreadyUsed(string Email);
        Task<User> Update(User user);
        Task<bool> Delete(User user);
    }
}
