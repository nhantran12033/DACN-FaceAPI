using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.Departments
{
    public abstract class DepartmentDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public string? Note { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}