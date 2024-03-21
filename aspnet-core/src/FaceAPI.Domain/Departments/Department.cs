using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace FaceAPI.Departments
{
    public abstract class DepartmentBase : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? Code { get; set; }

        [CanBeNull]
        public virtual string? Name { get; set; }

        public virtual DateTime Date { get; set; }

        [CanBeNull]
        public virtual string? Note { get; set; }

        public ICollection<DepartmentTitle> Titles { get; private set; }

        protected DepartmentBase()
        {

        }

        public DepartmentBase(Guid id, DateTime date, string? code = null, string? name = null, string? note = null)
        {

            Id = id;
            Date = date;
            Code = code;
            Name = name;
            Note = note;
            Titles = new Collection<DepartmentTitle>();
        }
        public virtual void AddTitle(Guid titleId)
        {
            Check.NotNull(titleId, nameof(titleId));

            if (IsInTitles(titleId))
            {
                return;
            }

            Titles.Add(new DepartmentTitle(Id, titleId));
        }

        public virtual void RemoveTitle(Guid titleId)
        {
            Check.NotNull(titleId, nameof(titleId));

            if (!IsInTitles(titleId))
            {
                return;
            }

            Titles.RemoveAll(x => x.TitleId == titleId);
        }

        public virtual void RemoveAllTitlesExceptGivenIds(List<Guid> titleIds)
        {
            Check.NotNullOrEmpty(titleIds, nameof(titleIds));

            Titles.RemoveAll(x => !titleIds.Contains(x.TitleId));
        }

        public virtual void RemoveAllTitles()
        {
            Titles.RemoveAll(x => x.DepartmentId == Id);
        }

        private bool IsInTitles(Guid titleId)
        {
            return Titles.Any(x => x.TitleId == titleId);
        }
    }
}