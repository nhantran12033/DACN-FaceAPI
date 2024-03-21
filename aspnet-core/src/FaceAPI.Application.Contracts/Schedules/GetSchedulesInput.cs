using Volo.Abp.Application.Dtos;
using System;

namespace FaceAPI.Schedules
{
    public abstract class GetSchedulesInputBase : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Code { get; set; }
        public string? Name { get; set; }
        public DateTime? DateFromMin { get; set; }
        public DateTime? DateFromMax { get; set; }
        public DateTime? DateToMin { get; set; }
        public DateTime? DateToMax { get; set; }
        public string? Note { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? ScheduleDetailId { get; set; }

        public GetSchedulesInputBase()
        {

        }
    }
}