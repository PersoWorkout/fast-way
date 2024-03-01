using Domain.Models;

namespace Application.Interfaces
{
    public interface IAuthorizationRepository
    {
        Task<Session?> GetByToken(string token);
        Task<Session> CreateSession(Session session);
        Task DestroyByToken(string token);
        Task DestroyByUser(Guid userId);
    }
}
