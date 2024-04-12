using System;

namespace FaceAPI.Timesheets
{
    public abstract class TimesheetExcelDtoBase
    {
        public string? Code { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public int HoursWork { get; set; }
        public string? Note { get; set; }
    }
}