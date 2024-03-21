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

        public ICollection<SalaryDepartment> Departments { get; private set; }

        protected SalaryBase()
        {

        }

        public SalaryBase(Guid id, double allowance, double basic, double bonus, string? code = null)
        {

            Id = id;
            Allowance = allowance;
            Basic = basic;
            Bonus = bonus;
            Total = Basic + Bonus + Allowance;
            Code = code;
            Departments = new Collection<SalaryDepartment>();
        }
        public virtual void AddDepartment(Guid departmentId)
        {
            Check.NotNull(departmentId, nameof(departmentId));

            if (IsInDepartments(departmentId))
            {
                return;
            }

            Departments.Add(new SalaryDepartment(Id, departmentId));
        }

        public virtual void RemoveDepartment(Guid departmentId)
        {
            Check.NotNull(departmentId, nameof(departmentId));

            if (!IsInDepartments(departmentId))
            {
                return;
            }

            Departments.RemoveAll(x => x.DepartmentId == departmentId);
        }

        public virtual void RemoveAllDepartmentsExceptGivenIds(List<Guid> departmentIds)
        {
            Check.NotNullOrEmpty(departmentIds, nameof(departmentIds));

            Departments.RemoveAll(x => !departmentIds.Contains(x.DepartmentId));
        }

        public virtual void RemoveAllDepartments()
        {
            Departments.RemoveAll(x => x.SalaryId == Id);
        }

        private bool IsInDepartments(Guid departmentId)
        {
            return Departments.Any(x => x.DepartmentId == departmentId);
        }
    }
}