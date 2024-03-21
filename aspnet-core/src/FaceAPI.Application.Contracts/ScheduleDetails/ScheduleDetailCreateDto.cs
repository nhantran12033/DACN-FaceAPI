using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FaceAPI.ScheduleDetails
{
    public abstract class ScheduleDetailCreateDtoBase
    {
        public string? Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string? Note { get; set; }
    }
}