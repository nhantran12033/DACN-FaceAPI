using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.ScheduleDetails
{
    public abstract class ScheduleDetailDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string? Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? Note { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}