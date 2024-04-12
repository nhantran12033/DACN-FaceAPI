using FaceAPI.Departments;
using FaceAPI.Titles;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace FaceAPI.Salaries
{
    public abstract class SalaryBase : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? Code { get; set; }

        public virtual double Allowance { get; set; }

        public virtual double Basic { get; set; }

        public virtual double Bonus { get; set; }

        public virtual double Total { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? TitleId { get; set; }

        protected SalaryBase()
        {

        }

        public SalaryBase(Guid id, Guid? departmentId, Guid? titleId, double allowance, double basic, double bonus, string? code = null)
        {

            Id = id;
            Allowance = allowance;
            Basic = basic;
            Bonus = bonus;
            Total = allowance + basic + bonus;
            Code = code;
            DepartmentId = departmentId;
            TitleId = titleId;
        }

    }
}