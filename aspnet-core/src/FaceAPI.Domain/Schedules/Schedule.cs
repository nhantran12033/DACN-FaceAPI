using FaceAPI.Staffs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace FaceAPI.Schedules
{
    public abstract class ScheduleBase : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? Code { get; set; }

        [CanBeNull]
        public virtual string? Name { get; set; }

        public virtual DateTime DateFrom { get; set; }

        public virtual DateTime DateTo { get; set; }

        [CanBeNull]
        public virtual string? Note { get; set; }
        public Guid StaffId { get; set; }
        public ICollection<ScheduleScheduleDetail> ScheduleDetails { get; private set; }

        protected ScheduleBase()
        {

        }

        public ScheduleBase(Guid id, Guid staffId, DateTime dateFrom, DateTime dateTo, string? code = null, string? name = null, string? note = null)
        {

            Id = id;
            DateFrom = dateFrom;
            DateTo = dateTo;
            Code = code;
            Name = name;
            Note = note;
            StaffId = staffId;
            ScheduleDetails = new Collection<ScheduleScheduleDetail>();
        }
        public virtual void AddScheduleDetail(Guid scheduleDetailId)
        {
            Check.NotNull(scheduleDetailId, nameof(scheduleDetailId));

            if (IsInScheduleDetails(scheduleDetailId))
            {
                return;
            }

            ScheduleDetails.Add(new ScheduleScheduleDetail(Id, scheduleDetailId));
        }

        public virtual void RemoveScheduleDetail(Guid scheduleDetailId)
        {
            Check.NotNull(scheduleDetailId, nameof(scheduleDetailId));

            if (!IsInScheduleDetails(scheduleDetailId))
            {
                return;
            }

            ScheduleDetails.RemoveAll(x => x.ScheduleDetailId == scheduleDetailId);
        }

        public virtual void RemoveAllScheduleDetailsExceptGivenIds(List<Guid> scheduleDetailIds)
        {
            Check.NotNullOrEmpty(scheduleDetailIds, nameof(scheduleDetailIds));

            ScheduleDetails.RemoveAll(x => !scheduleDetailIds.Contains(x.ScheduleDetailId));
        }

        public virtual void RemoveAllScheduleDetails()
        {
            ScheduleDetails.RemoveAll(x => x.ScheduleId == Id);
        }

        private bool IsInScheduleDetails(Guid scheduleDetailId)
        {
            return ScheduleDetails.Any(x => x.ScheduleDetailId == scheduleDetailId);
        }
    }
}