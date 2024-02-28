using Application.Commands.Authorization;
using Application.Interfaces;
using Domain.Abstractions;
using MediatR;

namespace Application.Handlers.Authorization
{
    public class LogoutHandler : IRequestHandler<LogoutCommand, Result<object>>
    {
        private readonly IAuthorizationRepository _authorizationRepository;

        public LogoutHandler(IAuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        public async Task<Result<object>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _authorizationRepository.DestroyByToken(request.Token);

            return Result<object>.Success();
        }
    }
}
