using AutoMapper;
using BaristaAPI.Models.Domain;
using BaristaAPI.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.DaysWorked, opt => opt.MapFrom(src => src.StartDate.HasValue ? (int?)(DateTime.Now - src.StartDate.Value).Days : null))
            .ForMember(dest => dest.Cafe, opt => opt.MapFrom(src => src.Cafe != null ? src.Cafe.Name : string.Empty));
            CreateMap<Employee, AddEmployeeDto>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeDto>().ReverseMap();

            CreateMap<Cafe, AddCafeDto>().ReverseMap();
            CreateMap<Cafe, CafeDto>().ReverseMap();
            CreateMap<Cafe, UpdateCafeDto>().ReverseMap();

        }
    }
}
