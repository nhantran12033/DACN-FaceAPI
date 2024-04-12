using FaceAPI.ScheduleFormats;

using System;
using System.Collections.Generic;

namespace FaceAPI.ScheduleDetails
{
    public abstract class ScheduleDetailWithNavigationPropertiesBase
    {
        public ScheduleDetail ScheduleDetail { get; set; } = null!;

        

        public List<ScheduleFormat> ScheduleFormats { get; set; } = null!;
        
    }
}