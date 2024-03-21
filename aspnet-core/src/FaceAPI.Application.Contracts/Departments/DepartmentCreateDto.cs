using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FaceAPI.Departments
{
    public abstract class DepartmentCreateDtoBase
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public string? Note { get; set; }
        public List<Guid> TitleIds { get; set; }
    }
}