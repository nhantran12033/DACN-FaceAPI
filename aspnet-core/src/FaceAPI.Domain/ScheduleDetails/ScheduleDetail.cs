using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace FaceAPI.ScheduleDetails
{
    public abstract class ScheduleDetailBase : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? Name { get; set; }

        public virtual DateTime FromDate { get; set; }

        public virtual DateTime ToDate { get; set; }

        [CanBeNull]
        public virtual string? Note { get; set; }

        public ICollection<ScheduleDetailScheduleFormat> ScheduleFormats { get; private set; }

        protected ScheduleDetailBase()
        {

        }

        public ScheduleDetailBase(Guid id, DateTime fromDate, DateTime toDate, string? name = null, string? note = null)
        {

            Id = id;
            FromDate = fromDate;
            ToDate = toDate;
            Name = name;
            Note = note;
            ScheduleFormats = new Collection<ScheduleDetailScheduleFormat>();
        }
        public virtual void AddScheduleFormat(Guid scheduleFormatId)
        {
            Check.NotNull(scheduleFormatId, nameof(scheduleFormatId));

            if (IsInScheduleFormats(scheduleFormatId))
            {
                return;
            }

            ScheduleFormats.Add(new ScheduleDetailScheduleFormat(Id, scheduleFormatId));
        }

        public virtual void RemoveScheduleFormat(Guid scheduleFormatId)
        {
            Check.NotNull(scheduleFormatId, nameof(scheduleFormatId));

            if (!IsInScheduleFormats(scheduleFormatId))
            {
                return;
            }

            ScheduleFormats.RemoveAll(x => x.ScheduleFormatId == scheduleFormatId);
        }

        public virtual void RemoveAllScheduleFormatsExceptGivenIds(List<Guid> scheduleFormatIds)
        {
            Check.NotNullOrEmpty(scheduleFormatIds, nameof(scheduleFormatIds));

            ScheduleFormats.RemoveAll(x => !scheduleFormatIds.Contains(x.ScheduleFormatId));
        }

        public virtual void RemoveAllScheduleFormats()
        {
            ScheduleFormats.RemoveAll(x => x.ScheduleDetailId == Id);
        }

        private bool IsInScheduleFormats(Guid scheduleFormatId)
        {
            return ScheduleFormats.Any(x => x.ScheduleFormatId == scheduleFormatId);
        }
    }
}