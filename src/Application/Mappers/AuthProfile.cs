using Application.Commands.Authorization;
using AutoMapper;
using Domain.DTOs.Authorization.Requests;
using Domain.ValueObjects;

namespace Application.Mappers
{
    public class AuthProfile: Profile
    {
        public AuthProfile() 
        {
            CreateMap<RegisterRequest, RegisterCommand>()
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(
                        src => EmailValueObject.Create(src.Email).Data))
                .ForMember(dest => dest.Password,
                    opt => opt.MapFrom(
                        src => PasswordValueObject.Create(src.Password).Data));

        }
    }
}
