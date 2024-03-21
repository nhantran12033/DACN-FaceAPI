using FaceAPI.Titles;
using FaceAPI.ScheduleDetails;
using FaceAPI.Schedules;
using FaceAPI.Salaries;
using FaceAPI.Positions;
using System;
using FaceAPI.Shared;
using Volo.Abp.AutoMapper;
using FaceAPI.Departments;
using AutoMapper;

namespace FaceAPI;

public class FaceAPIApplicationAutoMapperProfile : Profile
{
    public FaceAPIApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Department, DepartmentDto>();

        CreateMap<Position, PositionDto>();
        CreateMap<PositionWithNavigationProperties, PositionWithNavigationPropertiesDto>();
        CreateMap<Department, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Salary, SalaryDto>();
        CreateMap<Salary, SalaryExcelDto>();
        CreateMap<SalaryWithNavigationProperties, SalaryWithNavigationPropertiesDto>();

        CreateMap<Schedule, ScheduleDto>();
        CreateMap<Schedule, ScheduleExcelDto>();
        CreateMap<ScheduleWithNavigationProperties, ScheduleWithNavigationPropertiesDto>();

        CreateMap<ScheduleDetail, ScheduleDetailDto>();

        CreateMap<ScheduleDetail, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src =>
                $"{src.Name} ({src.From.ToString("yyyy-MM-dd HH:mm")} - {src.To.ToString("yyyy-MM-dd HH:mm")})"));


        CreateMap<Title, TitleDto>();

        CreateMap<DepartmentWithNavigationProperties, DepartmentWithNavigationPropertiesDto>();
        CreateMap<Title, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
    }
}