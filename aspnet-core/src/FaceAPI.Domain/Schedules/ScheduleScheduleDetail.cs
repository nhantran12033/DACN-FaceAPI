using System;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.Schedules
{
    public class ScheduleScheduleDetail : Entity
    {

        public Guid ScheduleId { get; protected set; }

        public Guid ScheduleDetailId { get; protected set; }

        private ScheduleScheduleDetail()
        {

        }

        public ScheduleScheduleDetail(Guid scheduleId, Guid scheduleDetailId)
        {
            ScheduleId = scheduleId;
            ScheduleDetailId = scheduleDetailId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    ScheduleId,
                    ScheduleDetailId
                };
        }
    }
}