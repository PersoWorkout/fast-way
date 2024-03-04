using AutoMapper;
using Domain.DTOs.Authorization;
using Domain.Models;

namespace Presentation.Presenters.Authorization
{
    public class RegisterPresenter(IMapper mapper)
    {
        private readonly IMapper _mapper = mapper;

        public object Json(Session data)
        {
            return new
            {
                data = _mapper.Map<ConnectedResponse>(data),
            };
        }
    }
}
