using FaceAPI.Departments;
using FaceAPI.Titles;
using FaceAPI.Timesheets;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace FaceAPI.Staffs
{
    public abstract class StaffWithNavigationPropertiesDtoBase
    {
        public StaffDto Staff { get; set; } = null!;

        public DepartmentDto Department { get; set; } = null!;
        public TitleDto Title { get; set; } = null!;
        public List<TimesheetDto> Timesheets { get; set; } = new List<TimesheetDto>();

    }
}