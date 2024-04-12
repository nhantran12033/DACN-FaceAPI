using FaceAPI.Staffs;
using FaceAPI.ScheduleDetails;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace FaceAPI.Schedules
{
    public abstract class ScheduleWithNavigationPropertiesDtoBase
    {
        public ScheduleDto Schedule { get; set; } = null!;

        public StaffDto Staff { get; set; } = null!;
        public List<ScheduleDetailDto> ScheduleDetails { get; set; } = new List<ScheduleDetailDto>();

    }
}