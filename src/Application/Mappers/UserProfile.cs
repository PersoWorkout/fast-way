using Application.Commands.Users;
using AutoMapper;
using Domain.DTOs.Users.Request;
using Domain.DTOs.Users.Response;
using Domain.Models;
using Domain.ValueObjects;

namespace Application.Mappers
{
    public class UserProfile: Profile
    {
        public UserProfile() 
        {
            CreateMap<CreateUserRequest, CreateUserCommand>();

            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(
                        src => EmailValueObject.Create(src.Email).Data))
                .ForMember(dest => dest.Password,
                    opt => opt.MapFrom(
                        src => PasswordValueObject.Create(src.Password).Data));

            CreateMap<User, UserForList>();
            CreateMap<User, UserDetails>()
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(
                        src => src.Email.Value));

        }
    }
}
