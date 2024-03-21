using System;

namespace FaceAPI.Schedules
{
    public abstract class ScheduleExcelDtoBase
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? Note { get; set; }
    }
}