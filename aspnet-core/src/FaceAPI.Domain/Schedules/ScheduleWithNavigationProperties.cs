using FaceAPI.Staffs;
using FaceAPI.ScheduleDetails;

using System;
using System.Collections.Generic;

namespace FaceAPI.Schedules
{
    public abstract class ScheduleWithNavigationPropertiesBase
    {
        public Schedule Schedule { get; set; } = null!;

        public Staff Staff { get; set; } = null!;
        

        public List<ScheduleDetail> ScheduleDetails { get; set; } = null!;
        
    }
}