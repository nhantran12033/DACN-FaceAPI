using FaceAPI.Schedules;
using FaceAPI.ScheduleDetails;
using FaceAPI.ScheduleFormats;

using System;
using System.Collections.Generic;

namespace FaceAPI.Timesheets
{
    public abstract class TimesheetWithNavigationPropertiesBase
    {
        public Timesheet Timesheet { get; set; } = null!;

        public Schedule Schedule { get; set; } = null!;
        public ScheduleDetail ScheduleDetail { get; set; } = null!;
        public ScheduleFormat ScheduleFormat { get; set; } = null!;
        

        
    }
}