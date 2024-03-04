using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Queries;
public class GetUsersQuery: IRequest<Result<List<User>>> { }
