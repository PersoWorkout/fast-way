using Application.Interfaces;
using Domain.Models;
using Domain.ValueObjects;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> Create(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EmailAlreadyUsed(string Email)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmail(EmailValueObject email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
