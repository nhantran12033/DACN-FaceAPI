using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.ScheduleDetails
{
    public abstract class ScheduleDetailUpdateDtoBase : IHasConcurrencyStamp
    {
        public string? Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? Note { get; set; }
        public List<Guid> ScheduleFormatIds { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}