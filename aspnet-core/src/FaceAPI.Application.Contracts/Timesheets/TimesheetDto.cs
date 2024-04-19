using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.Timesheets
{
    public abstract class TimesheetDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string? Image { get; set; }
        public string? Code { get; set; }
        public DateTime Time { get; set; }
        public string? Note { get; set; }
        public Guid? ScheduleId { get; set; }
        public Guid? ScheduleDetailId { get; set; }
        public Guid? ScheduleFormatId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}