using FaceAPI.Titles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace FaceAPI.Departments
{
    public abstract class DepartmentManagerBase : DomainService
    {
        protected IDepartmentRepository _departmentRepository;
        protected IRepository<Title, Guid> _titleRepository;

        public DepartmentManagerBase(IDepartmentRepository departmentRepository,
        IRepository<Title, Guid> titleRepository)
        {
            _departmentRepository = departmentRepository;
            _titleRepository = titleRepository;
        }

        public virtual async Task<Department> CreateAsync(
        List<Guid> titleIds,
        DateTime date, string? code = null, string? name = null, string? note = null)
        {
            Check.NotNull(date, nameof(date));

            var department = new Department(
             GuidGenerator.Create(),
             date, code, name, note
             );

            await SetTitlesAsync(department, titleIds);

            return await _departmentRepository.InsertAsync(department);
        }

        public virtual async Task<Department> UpdateAsync(
            Guid id,
            List<Guid> titleIds,
        DateTime date, string? code = null, string? name = null, string? note = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(date, nameof(date));

            var queryable = await _departmentRepository.WithDetailsAsync(x => x.Titles);
            var query = queryable.Where(x => x.Id == id);

            var department = await AsyncExecuter.FirstOrDefaultAsync(query);

            department.Date = date;
            department.Code = code;
            department.Name = name;
            department.Note = note;

            await SetTitlesAsync(department, titleIds);

            department.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _departmentRepository.UpdateAsync(department);
        }

        private async Task SetTitlesAsync(Department department, List<Guid> titleIds)
        {
            if (titleIds == null || !titleIds.Any())
            {
                department.RemoveAllTitles();
                return;
            }

            var query = (await _titleRepository.GetQueryableAsync())
                .Where(x => titleIds.Contains(x.Id))
                .Select(x => x.Id);

            var titleIdsInDb = await AsyncExecuter.ToListAsync(query);
            if (!titleIdsInDb.Any())
            {
                return;
            }

            department.RemoveAllTitlesExceptGivenIds(titleIdsInDb);

            foreach (var titleId in titleIdsInDb)
            {
                department.AddTitle(titleId);
            }
        }

    }
}