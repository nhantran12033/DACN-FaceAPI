using Volo.Abp.Application.Dtos;
using System;

namespace FaceAPI.ScheduleDetails
{
    public abstract class GetScheduleDetailsInputBase : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public DateTime? FromDateMin { get; set; }
        public DateTime? FromDateMax { get; set; }
        public DateTime? ToDateMin { get; set; }
        public DateTime? ToDateMax { get; set; }
        public string? Note { get; set; }
        public Guid? ScheduleFormatId { get; set; }

        public GetScheduleDetailsInputBase()
        {

        }
    }
}