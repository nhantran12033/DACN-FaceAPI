using FaceAPI.ScheduleFormats;
using FaceAPI.Staffs;
using FaceAPI.Timesheets;
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

        CreateMap<Salary, SalaryDto>();
        CreateMap<Salary, SalaryExcelDto>();

        CreateMap<Schedule, ScheduleDto>();
        CreateMap<Schedule, ScheduleExcelDto>();
        CreateMap<ScheduleWithNavigationProperties, ScheduleWithNavigationPropertiesDto>();

        CreateMap<ScheduleDetail, ScheduleDetailDto>();

        CreateMap<ScheduleDetail, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src =>
                $"{src.Name
            
} - {src.FromDate.ToString("yyyy-MM-dd")} - {src.ToDate.ToString("yyyy-MM-dd")}"));

        CreateMap<ScheduleDetailWithNavigationProperties, ScheduleDetailWithNavigationPropertiesDto>();
        CreateMap<ScheduleFormat, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src =>
                $"{src.Name} - {src.FromHours.ToString("yyyy-MM-dd")} - {src.ToHours.ToString("yyyy-MM-dd")}"));
        CreateMap<ScheduleFormat, ScheduleFormatDto>();
        CreateMap<SalaryWithNavigationProperties, SalaryWithNavigationPropertiesDto>();
        CreateMap<Department, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
        CreateMap<Staff, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));
        CreateMap<Staff, StaffDto>();
        CreateMap<Staff, StaffExcelDto>();
        CreateMap<StaffWithNavigationProperties, StaffWithNavigationPropertiesDto>();
        CreateMap<Timesheet, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<Timesheet, TimesheetDto>();
        CreateMap<Timesheet, TimesheetExcelDto>();
        CreateMap<TimesheetWithNavigationProperties, TimesheetWithNavigationPropertiesDto>();
        CreateMap<Schedule, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<Title, TitleDto>();

        CreateMap<DepartmentWithNavigationProperties, DepartmentWithNavigationPropertiesDto>();
        CreateMap<Title, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
    }
}