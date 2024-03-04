using AutoMapper;
using Domain.Models;

namespace Presentation.Presenters.Users
{
    public class CreateUserPresenter(IMapper mapper)
    {
        private readonly IMapper _mapper = mapper;

        public object Json(User user)
        {
            return new
            {
                data = _mapper.Map<User>(user)
            };
        }
    }
}
