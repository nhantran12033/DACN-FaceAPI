using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.Salaries
{
    public abstract class SalaryDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string? Code { get; set; }
        public double Allowance { get; set; }
        public double Basic { get; set; }
        public double Bonus { get; set; }
        public double Total { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? TitleId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}