using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly AuthDbContext _dbContext;

        public AuthorizationRepository(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Session> CreateSession(Guid userId, string token)
        {
            var session = new Session
            {
                UserId = userId,
                Token = token,
                ExpiredAt = DateTime.Now.AddHours(1),
            };

            var result = await _dbContext.Sessions.AddAsync(session);
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }
        public Task<Session> GetByToken(string token)
        {
            throw new NotImplementedException();
        }

        public Task DestroyByToken(string token)
        {
            throw new NotImplementedException();
        }

        public Task DestroyByUser(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
