using Volo.Abp.Application.Dtos;
using System;

namespace FaceAPI.ScheduleFormats
{
    public abstract class GetScheduleFormatsInputBase : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public DateTime? DateMin { get; set; }
        public DateTime? DateMax { get; set; }
        public int? FromHoursMin { get; set; }
        public int? FromHoursMax { get; set; }
        public int? ToHoursMin { get; set; }
        public int? ToHoursMax { get; set; }
        public string? Note { get; set; }

        public GetScheduleFormatsInputBase()
        {

        }
    }
}