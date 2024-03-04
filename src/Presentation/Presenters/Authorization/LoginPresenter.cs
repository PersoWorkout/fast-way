using AutoMapper;
using Domain.DTOs.Authorization;
using Domain.Models;

namespace Presentation.Presenters.Authorization
{
    public class LoginPresenter
    {
        private readonly IMapper _mapper;

        public LoginPresenter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public object Json(Session data)
        {
            return new
            {
                data = _mapper.Map<ConnectedResponse>(data),
            };
        }
    }
}
