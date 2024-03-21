using FaceAPI.Departments;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace FaceAPI.Positions
{
    public abstract class PositionBase : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? Code { get; set; }

        [CanBeNull]
        public virtual string? Name { get; set; }

        [CanBeNull]
        public virtual string? Note { get; set; }
        public Guid? DepartmentId { get; set; }

        protected PositionBase()
        {

        }

        public PositionBase(Guid id, Guid? departmentId, string? code = null, string? name = null, string? note = null)
        {

            Id = id;
            Code = code;
            Name = name;
            Note = note;
            DepartmentId = departmentId;
        }

    }
}