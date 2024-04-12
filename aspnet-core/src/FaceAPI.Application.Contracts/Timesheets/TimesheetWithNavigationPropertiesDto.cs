using FaceAPI.Schedules;
using FaceAPI.ScheduleDetails;
using FaceAPI.ScheduleFormats;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace FaceAPI.Timesheets
{
    public abstract class TimesheetWithNavigationPropertiesDtoBase
    {
        public TimesheetDto Timesheet { get; set; } = null!;

        public ScheduleDto Schedule { get; set; } = null!;
        public ScheduleDetailDto ScheduleDetail { get; set; } = null!;
        public ScheduleFormatDto ScheduleFormat { get; set; } = null!;

    }
}