using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.Schedules
{
    public abstract class ScheduleUpdateDtoBase : IHasConcurrencyStamp
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? Note { get; set; }
        public Guid? DepartmentId { get; set; }
        public List<Guid> ScheduleDetailIds { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}