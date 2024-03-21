using FaceAPI.Titles;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace FaceAPI.Departments
{
    public abstract class DepartmentWithNavigationPropertiesDtoBase
    {
        public DepartmentDto Department { get; set; } = null!;

        public List<TitleDto> Titles { get; set; } = new List<TitleDto>();

    }
}