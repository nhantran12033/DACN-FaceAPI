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

        public virtual DateTime From { get; set; }

        public virtual DateTime To { get; set; }

        [CanBeNull]
        public virtual string? Note { get; set; }

        protected ScheduleDetailBase()
        {

        }

        public ScheduleDetailBase(Guid id, DateTime from, DateTime to, string? name = null, string? note = null)
        {

            Id = id;
            From = from;
            To = to;
            Name = name;
            Note = note;
        }

    }
}