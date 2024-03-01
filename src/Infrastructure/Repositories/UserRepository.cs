using Application.Interfaces;
using Domain.Models;
using Domain.ValueObjects;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Users.AsNoTracking();
        }

        public async Task<List<User>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> Create(User user)
        {
            var createdUser = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return createdUser.Entity;
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<bool> EmailAlreadyUsed(EmailValueObject email)
        {
            return await _dbContext.Users.AnyAsync(
                x => x.Email.Value == email.Value);
        }

        public async Task<User?> GetByEmail(EmailValueObject email)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(
                    x => x.Email.Value == email.Value);
        }

        public async Task<User> Update(User user)
        {
            var result = _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<int> Delete(User user)
        {
            _dbContext.Users.Remove(user);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
