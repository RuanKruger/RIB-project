using AutoMapper;
using AcmeSoft.Core.DTOs;
using AcmeSoft.Core.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AcmeSoft.Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDto>();

            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Person!.LastName))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Person!.FirstName))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.Person!.BirthDate));

            CreateMap<CreateEmployeeDto, Person>();
            CreateMap<CreateEmployeeDto, Employee>();

            CreateMap<UpdateEmployeeDto, Person>();
            CreateMap<UpdateEmployeeDto, Employee>();
        }
    }
}