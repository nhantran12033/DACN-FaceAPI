using Volo.Abp.Application.Dtos;
using System;

namespace FaceAPI.Timesheets
{
    public abstract class TimesheetExcelDownloadDtoBase
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? Code { get; set; }
        public DateTime? TimeInMin { get; set; }
        public DateTime? TimeInMax { get; set; }
        public DateTime? TimeOutMin { get; set; }
        public DateTime? TimeOutMax { get; set; }
        public int? HoursWorkMin { get; set; }
        public int? HoursWorkMax { get; set; }
        public string? Note { get; set; }
        public Guid? ScheduleId { get; set; }
        public Guid? ScheduleDetailId { get; set; }
        public Guid? ScheduleFormatId { get; set; }

        public TimesheetExcelDownloadDtoBase()
        {

        }
    }
}