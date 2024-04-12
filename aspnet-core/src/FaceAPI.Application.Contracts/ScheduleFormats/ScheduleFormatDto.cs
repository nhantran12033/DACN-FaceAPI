using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.ScheduleFormats
{
    public abstract class ScheduleFormatDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public int FromHours { get; set; }
        public int ToHours { get; set; }
        public string? Note { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}