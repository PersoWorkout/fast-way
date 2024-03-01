using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly AuthDbContext _dbContext;

        public AuthorizationRepository(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Session> CreateSession(Session session)
        {
            var result = await _dbContext.Sessions.AddAsync(session);
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }
        public async Task<Session?> GetByToken(string token)
        {
            return await _dbContext.Sessions.FirstOrDefaultAsync(
                 session => session.Token == token);
        }

        public async Task DestroyByToken(string token)
        {
            var session = await _dbContext.Sessions
                .FirstOrDefaultAsync(x => x.Token == token);

            if(session is not null)
            {
                _dbContext.Remove(session);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DestroyByUser(Guid userId)
        {
            var sessions = await _dbContext.Sessions.Where(
                x => x.UserId == userId).ToListAsync();

            _dbContext.Sessions.RemoveRange(sessions);

            await _dbContext.SaveChangesAsync();
        }
    }
}
