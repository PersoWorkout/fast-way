using AutoMapper;
using Domain.DTOs.Users.Response;
using Domain.Models;

namespace Presentation.Presenters.Users
{
    public class GetUsersPresenter(IMapper mapper)
    {
        private readonly IMapper _mapper = mapper;

        public object Json(List<User> users)
        {
            return new
            {
                data = users.Select(_mapper.Map<UserForList>).ToList(),
            };
        }
    }
}
