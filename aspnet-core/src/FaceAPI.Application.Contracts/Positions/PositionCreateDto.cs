using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FaceAPI.Positions
{
    public abstract class PositionCreateDtoBase
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Note { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}