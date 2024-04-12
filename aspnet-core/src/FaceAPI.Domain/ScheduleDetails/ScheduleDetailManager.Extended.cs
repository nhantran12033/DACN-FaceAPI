using FaceAPI.ScheduleFormats;
using System;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;

namespace FaceAPI.ScheduleDetails
{
    public class ScheduleDetailManager : ScheduleDetailManagerBase
    {
        //<suite-custom-code-autogenerated>
        public ScheduleDetailManager(IScheduleDetailRepository scheduleDetailRepository,
        IRepository<ScheduleFormat, Guid> scheduleFormatRepository)
            : base(scheduleDetailRepository, scheduleFormatRepository)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}