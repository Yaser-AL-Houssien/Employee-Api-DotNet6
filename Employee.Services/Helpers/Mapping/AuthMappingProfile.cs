using AutoMapper;
using Employee.Core.DTOs;
using Employee.Core.Entities.Models;

namespace Employee.Services.Helpers.Mapping;
public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<RegisterDto, ApplicationUser>();
    }
}
