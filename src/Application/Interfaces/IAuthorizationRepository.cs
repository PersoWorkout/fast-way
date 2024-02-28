using Domain.Models;

namespace Application.Interfaces
{
    public interface IAuthorizationRepository
    {
        Task<Session> GetByToken(string token);
        Task<Session> CreateSession(Guid userId, string token);
        Task DestroyByToken(string token);
        Task DestroyByUser(Guid userId);
    }
}
