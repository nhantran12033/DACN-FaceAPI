using FaceAPI.Departments;
using FaceAPI.Titles;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace FaceAPI.Salaries
{
    public abstract class SalaryWithNavigationPropertiesDtoBase
    {
        public SalaryDto Salary { get; set; } = null!;

        public DepartmentDto Department { get; set; } = null!;
        public TitleDto Title { get; set; } = null!;

    }
}