using AutoMapper;
using FirstProject.DTOs.SchoolDTOs;
using FirstProject.DTOs.StudentDTOs;
using FirstProject.Entities;

namespace FirstProject.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<School, SchoolGetDTO>().ReverseMap();

        CreateMap<Student, StudentGetDTO>()
            .ForMember(dest => dest.SchoolName, options => options.MapFrom(src => src.School.SchoolName))
            .ReverseMap();
    }
}
