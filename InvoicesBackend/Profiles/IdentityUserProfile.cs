using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace InvoicesBackend.Profiles
{
    public class IdentityUserProfile : Profile
    {
        public IdentityUserProfile()
        {
            CreateMap<Models.RegisterRequestDTO, IdentityUser>()
                .ForSourceMember(src => src.Password, opt => opt.DoNotValidate());
        }
    }
}