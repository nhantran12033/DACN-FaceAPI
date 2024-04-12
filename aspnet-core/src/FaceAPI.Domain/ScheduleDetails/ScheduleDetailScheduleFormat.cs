using System;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.ScheduleDetails
{
    public class ScheduleDetailScheduleFormat : Entity
    {

        public Guid ScheduleDetailId { get; protected set; }

        public Guid ScheduleFormatId { get; protected set; }

        private ScheduleDetailScheduleFormat()
        {

        }

        public ScheduleDetailScheduleFormat(Guid scheduleDetailId, Guid scheduleFormatId)
        {
            ScheduleDetailId = scheduleDetailId;
            ScheduleFormatId = scheduleFormatId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    ScheduleDetailId,
                    ScheduleFormatId
                };
        }
    }
}