using Application.Interfaces;
using Application.Queries;
using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Users;

public class GetUsersHandler(IUserRepository userRepository) : 
    IRequestHandler<GetUsersQuery, Result<List<User>>>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<List<User>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return Result<List<User>>
            .Success(await _userRepository.GetAll());
    }
}

