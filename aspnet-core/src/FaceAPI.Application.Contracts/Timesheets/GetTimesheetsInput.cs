using Volo.Abp.Application.Dtos;
using System;

namespace FaceAPI.Timesheets
{
    public abstract class GetTimesheetsInputBase : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Image { get; set; }
        public string? Code { get; set; }
        public DateTime? TimeMin { get; set; }
        public DateTime? TimeMax { get; set; }
        public string? Note { get; set; }
        public Guid? ScheduleId { get; set; }
        public Guid? ScheduleDetailId { get; set; }
        public Guid? ScheduleFormatId { get; set; }

        public GetTimesheetsInputBase()
        {

        }
    }
}