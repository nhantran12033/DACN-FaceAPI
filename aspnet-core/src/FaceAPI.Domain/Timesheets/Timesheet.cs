using FaceAPI.Schedules;
using FaceAPI.ScheduleDetails;
using FaceAPI.ScheduleFormats;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace FaceAPI.Timesheets
{
    public abstract class TimesheetBase : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? Image { get; set; }

        [CanBeNull]
        public virtual string? Code { get; set; }

        public virtual DateTime Time { get; set; }

        [CanBeNull]
        public virtual string? Note { get; set; }
        public Guid? ScheduleId { get; set; }
        public Guid? ScheduleDetailId { get; set; }
        public Guid? ScheduleFormatId { get; set; }

        protected TimesheetBase()
        {

        }

        public TimesheetBase(Guid id, Guid? scheduleId, Guid? scheduleDetailId, Guid? scheduleFormatId, DateTime time, string? image = null, string? code = null, string? note = null)
        {

            Id = id;
            Time = time;
            Image = image;
            Code = code;
            Note = note;
            ScheduleId = scheduleId;
            ScheduleDetailId = scheduleDetailId;
            ScheduleFormatId = scheduleFormatId;
        }

    }
}