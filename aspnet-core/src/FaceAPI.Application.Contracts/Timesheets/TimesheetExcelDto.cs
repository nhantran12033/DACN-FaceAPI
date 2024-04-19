using System;

namespace FaceAPI.Timesheets
{
    public abstract class TimesheetExcelDtoBase
    {
        public string? Image { get; set; }
        public string? Code { get; set; }
        public DateTime Time { get; set; }
        public string? Note { get; set; }
    }
}