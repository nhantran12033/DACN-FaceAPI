using FaceAPI.Departments;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace FaceAPI.Salaries
{
    public abstract class SalaryWithNavigationPropertiesDtoBase
    {
        public SalaryDto Salary { get; set; } = null!;

        public List<DepartmentDto> Departments { get; set; } = new List<DepartmentDto>();

    }
}