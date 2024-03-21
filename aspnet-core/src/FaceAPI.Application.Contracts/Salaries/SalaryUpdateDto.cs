using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.Salaries
{
    public abstract class SalaryUpdateDtoBase : IHasConcurrencyStamp
    {
        public string? Code { get; set; }
        public double Allowance { get; set; }
        public double Basic { get; set; }
        public double Bonus { get; set; }
        public List<Guid> DepartmentIds { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}