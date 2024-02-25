using Application.Interfaces;
using Application.Queries;
using AutoMapper;
using Domain.Abstractions;
using Domain.DTOs.Users.Response;
using MediatR;

namespace Application.Handlers.Users;

public class GetUsersHandler(
    IUserRepository userRepository, 
    IMapper mapper) : IRequestHandler<GetUsersQuery, Result<List<UserForList>>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<UserForList>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var data = await _userRepository.GetAll();
        return Result<List<UserForList>>
                .Success(data.Select(
                    _mapper.Map<UserForList>)
                .ToList());
    }
}

