using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.Positions
{
    public abstract class PositionDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Note { get; set; }
        public Guid? DepartmentId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}