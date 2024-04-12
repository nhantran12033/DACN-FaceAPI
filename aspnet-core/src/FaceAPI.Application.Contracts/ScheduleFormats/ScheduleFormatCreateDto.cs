using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FaceAPI.ScheduleFormats
{
    public abstract class ScheduleFormatCreateDtoBase
    {
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public int FromHours { get; set; }
        public int ToHours { get; set; }
        public string? Note { get; set; }
    }
}