using AutoMapper;
using Domain.DTOs.Users.Response;
using Domain.Models;

namespace Application.Mappers
{
    public class UserProfile: Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserForList>();
            CreateMap<User, UserDetails>()
                .ForMember(src => src.Email,
                    opt => opt.MapFrom(
                        src => src.Email.Value));

        }
    }
}
