using FaceAPI.Departments;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace FaceAPI.Positions
{
    public abstract class PositionWithNavigationPropertiesDtoBase
    {
        public PositionDto Position { get; set; } = null!;

        public DepartmentDto Department { get; set; } = null!;

    }
}