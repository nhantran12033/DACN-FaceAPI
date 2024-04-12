using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FaceAPI.Timesheets
{
    public abstract class TimesheetCreateDtoBase
    {
        public string? Code { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public int HoursWork { get; set; }
        public string? Note { get; set; }
        public Guid? ScheduleId { get; set; }
        public Guid? ScheduleDetailId { get; set; }
        public Guid? ScheduleFormatId { get; set; }
    }
}