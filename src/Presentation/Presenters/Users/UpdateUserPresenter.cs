using AutoMapper;
using Domain.DTOs.Users.Response;
using Domain.Models;

namespace Presentation.Presenters.Users
{
    public class UpdateUserPresenter(IMapper mapper)
    {
        private readonly IMapper _mapper = mapper;

        public object Json(User user)
        {
            return new
            {
                data = _mapper.Map<UserDetails>(user),
            };
        }
    }
}
