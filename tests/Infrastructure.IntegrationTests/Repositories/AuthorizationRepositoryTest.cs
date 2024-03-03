using Application.Interfaces;
using Application.Services.Authorization;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IntegrationTests.Repositories
{
    public class AuthorizationRepositoryTest : BaseIntegrationTest
    {
        private readonly HashService _hashService;
        private readonly IAuthorizationRepository _authRepository;
        private readonly AuthDbContext _dbContext;
        public AuthorizationRepositoryTest(IntegrationWebApplicationFactory factory) : base(factory)
        {
            _hashService = new HashService();

            _authRepository = _scope.ServiceProvider
                .GetRequiredService<IAuthorizationRepository>();

            _dbContext = _scope.ServiceProvider
                .GetRequiredService<AuthDbContext>();
        }

        [Fact]
        public async Task CreateSession_ShouldeCreateSession()
        {
            //Arrange
            var userId = Guid.NewGuid();
            string token = TokenService.Generate();

            //Act
            var session = await _authRepository
                .CreateSession(new Session(
                    userId, 
                    _hashService.Hash(token)!));

            //Assert
            var existingSession = await _dbContext.Sessions
                .FirstOrDefaultAsync(x => _hashService.Verify(token, x.Token));

            Assert.NotNull(existingSession);
            Assert.Equal(userId, session.UserId);
        }

        [Fact]
        public async Task GetByToken_ShouldReturnSession_WhenTokenExist()
        {
            //Arrange
            var token = TokenService.Generate();
            var session = new Session(
                    Guid.NewGuid(),
                    _hashService.Hash(token)!);

            var existingSession = await _dbContext
                .AddAsync(session);

            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _authRepository
                .GetByToken(token);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(existingSession.Entity.UserId, session.UserId);
        }

        [Fact]
        public async Task GetByToken_ShouldReturnNull_WhenTokenNotExist()
        {
            //Act
            //Arrange
            var session = await _authRepository
                .GetByToken(TokenService.Generate());

            //Assert
            Assert.Null(session);
        }

        [Fact]
        public async Task DestroyByToken_ShouldDeleteSession()
        {
            //Arrange
            var existingSession = await _dbContext.AddAsync(
                new Session(
                    Guid.NewGuid(),
                    _hashService.Hash(TokenService.Generate())!));

            await _dbContext.SaveChangesAsync();

            //Act
            await _authRepository.DestroyByToken(existingSession.Entity.Token);

            //Assert
            var existInDb = await _dbContext.Sessions.AnyAsync(
                x => _hashService.Verify(existingSession.Entity.Token, x.Token));

            Assert.False(existInDb);
        }

        [Fact]
        public async Task DestroyByUser_ShouldDeleteSessionsOfUser()
        {
            //Arrange
            var userId = Guid.NewGuid();

            var firstSession = await _dbContext.AddAsync(
                new Session(
                    userId,
                    _hashService.Hash(TokenService.Generate())!));

            var secondSession = await _dbContext.AddAsync(
                new Session(
                    userId,
                    _hashService.Hash(TokenService.Generate())!));

            await _dbContext.SaveChangesAsync();

            //Act
            await _authRepository.DestroyByUser(userId);

            //Assert
            var sessionsCount = await _dbContext.Sessions.Where(
                x => x.UserId == userId)
                .CountAsync();

            Assert.Equal(0, sessionsCount);
        }
    }
}
