using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.Departments
{
    public abstract class DepartmentUpdateDtoBase : IHasConcurrencyStamp
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public string? Note { get; set; }
        public List<Guid> TitleIds { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}