using AutoMapper;
using Employee.Core.DTOs.Employee;
using Employee.Services.Extensions;

namespace Employee.Services.Helpers.Mapping
{
    public class EmployeeMappingProfile:Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee.Core.Entities.Models.Employee, EmployeeDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(
               src => src.Name.Trim().CapitalizeFistLitter()));
            CreateMap<EmployeeCreationDto, Employee.Core.Entities.Models.Employee>();
            CreateMap<EmployeeUpdateDto, Employee.Core.Entities.Models.Employee>();
        }
    }
}
