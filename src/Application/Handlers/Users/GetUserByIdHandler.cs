using Application.Interfaces;
using Application.Queries;
using AutoMapper;
using Domain.Abstractions;
using Domain.DTOs.Users.Response;
using Domain.Errors;
using MediatR;

namespace Application.Handlers.Users
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Result<UserDetails>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserDetails>> Handle(
            GetUserByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.Id);
            if (user is null)
                return Result<UserDetails>.Failure(
                    UserErrors.NotFound(request.Id.ToString()));

            return Result<UserDetails>.Success(
                _mapper.Map<UserDetails>(user));
        }
    }
}
