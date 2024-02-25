using Domain.Abstractions;
using Domain.DTOs.Users.Response;
using MediatR;

namespace Application.Queries;
public class GetUsersQuery: IRequest<Result<List<UserForList>>> { }
