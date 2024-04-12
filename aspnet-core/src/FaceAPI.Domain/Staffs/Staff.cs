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

namespace FaceAPI.Staffs
{
    public abstract class StaffBase : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? Image { get; set; }

        [CanBeNull]
        public virtual string? Code { get; set; }

        [CanBeNull]
        public virtual string? Name { get; set; }

        [CanBeNull]
        public virtual string? Sex { get; set; }

        public virtual DateTime Birthday { get; set; }

        public virtual DateTime StartWork { get; set; }

        [CanBeNull]
        public virtual string? Phone { get; set; }

        [CanBeNull]
        public virtual string? Email { get; set; }

        [CanBeNull]
        public virtual string? Address { get; set; }

        public virtual int Debt { get; set; }

        [CanBeNull]
        public virtual string? Note { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? TitleId { get; set; }
        public ICollection<StaffTimesheet> Timesheets { get; private set; }

        protected StaffBase()
        {

        }

        public StaffBase(Guid id, Guid? departmentId, Guid? titleId, DateTime birthday, DateTime startWork, int debt, string? image = null, string? code = null, string? name = null, string? sex = null, string? phone = null, string? email = null, string? address = null, string? note = null)
        {

            Id = id;
            Birthday = birthday;
            StartWork = startWork;
            Debt = debt;
            Image = image;
            Code = code;
            Name = name;
            Sex = sex;
            Phone = phone;
            Email = email;
            Address = address;
            Note = note;
            DepartmentId = departmentId;
            TitleId = titleId;
            Timesheets = new Collection<StaffTimesheet>();
        }
        public virtual void AddTimesheet(Guid timesheetId)
        {
            Check.NotNull(timesheetId, nameof(timesheetId));

            if (IsInTimesheets(timesheetId))
            {
                return;
            }

            Timesheets.Add(new StaffTimesheet(Id, timesheetId));
        }

        public virtual void RemoveTimesheet(Guid timesheetId)
        {
            Check.NotNull(timesheetId, nameof(timesheetId));

            if (!IsInTimesheets(timesheetId))
            {
                return;
            }

            Timesheets.RemoveAll(x => x.TimesheetId == timesheetId);
        }

        public virtual void RemoveAllTimesheetsExceptGivenIds(List<Guid> timesheetIds)
        {
            Check.NotNullOrEmpty(timesheetIds, nameof(timesheetIds));

            Timesheets.RemoveAll(x => !timesheetIds.Contains(x.TimesheetId));
        }

        public virtual void RemoveAllTimesheets()
        {
            Timesheets.RemoveAll(x => x.StaffId == Id);
        }

        private bool IsInTimesheets(Guid timesheetId)
        {
            return Timesheets.Any(x => x.TimesheetId == timesheetId);
        }
    }
}