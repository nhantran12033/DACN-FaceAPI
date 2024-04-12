using FaceAPI.ScheduleFormats;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace FaceAPI.ScheduleDetails
{
    public abstract class ScheduleDetailWithNavigationPropertiesDtoBase
    {
        public ScheduleDetailDto ScheduleDetail { get; set; } = null!;

        public List<ScheduleFormatDto> ScheduleFormats { get; set; } = new List<ScheduleFormatDto>();

    }
}