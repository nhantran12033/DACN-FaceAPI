using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FaceAPI.Schedules
{
    public abstract class ScheduleCreateDtoBase
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? Note { get; set; }
        public Guid StaffId { get; set; }
        public List<Guid> ScheduleDetailIds { get; set; }
    }
}