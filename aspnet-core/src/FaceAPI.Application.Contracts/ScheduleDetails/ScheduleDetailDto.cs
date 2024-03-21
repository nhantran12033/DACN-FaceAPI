using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.ScheduleDetails
{
    public abstract class ScheduleDetailDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string? Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string? Note { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}