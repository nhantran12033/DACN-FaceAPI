using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.ScheduleFormats
{
    public abstract class ScheduleFormatUpdateDtoBase : IHasConcurrencyStamp
    {
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public int FromHours { get; set; }
        public int ToHours { get; set; }
        public string? Note { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}