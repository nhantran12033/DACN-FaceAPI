using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace FaceAPI.ScheduleFormats
{
    public abstract class ScheduleFormatBase : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? Name { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual int FromHours { get; set; }

        public virtual int ToHours { get; set; }

        [CanBeNull]
        public virtual string? Note { get; set; }

        protected ScheduleFormatBase()
        {

        }

        public ScheduleFormatBase(Guid id, DateTime date, int fromHours, int toHours, string? name = null, string? note = null)
        {

            Id = id;
            Date = date;
            FromHours = fromHours;
            ToHours = toHours;
            Name = name;
            Note = note;
        }

    }
}