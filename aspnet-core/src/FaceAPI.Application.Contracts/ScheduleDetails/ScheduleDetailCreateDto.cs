using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FaceAPI.ScheduleDetails
{
    public abstract class ScheduleDetailCreateDtoBase
    {
        public string? Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? Note { get; set; }
        public List<Guid> ScheduleFormatIds { get; set; }
    }
}