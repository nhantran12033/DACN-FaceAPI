using FaceAPI.Titles;
using FaceAPI.Titles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using FaceAPI.EntityFrameworkCore;

namespace FaceAPI.Departments
{
    public abstract class EfCoreDepartmentRepositoryBase : EfCoreRepository<FaceAPIDbContext, Department, Guid>
    {
        public EfCoreDepartmentRepositoryBase(IDbContextProvider<FaceAPIDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<DepartmentWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id).Include(x => x.Titles)
                .Select(department => new DepartmentWithNavigationProperties
                {
                    Department = department,
                    Titles = (from departmentTitles in department.Titles
                              join _title in dbContext.Set<Title>() on departmentTitles.TitleId equals _title.Id
                              select _title).ToList()
                }).FirstOrDefault();
        }

        public virtual async Task<List<DepartmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            string? note = null,
            Guid? titleId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, dateMin, dateMax, note, titleId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DepartmentConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<DepartmentWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from department in (await GetDbSetAsync())

                   select new DepartmentWithNavigationProperties
                   {
                       Department = department,
                       Titles = new List<Title>()
                   };
        }

        protected virtual IQueryable<DepartmentWithNavigationProperties> ApplyFilter(
            IQueryable<DepartmentWithNavigationProperties> query,
            string? filterText,
            string? code = null,
            string? name = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            string? note = null,
            Guid? titleId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Department.Code!.Contains(filterText!) || e.Department.Name!.Contains(filterText!) || e.Department.Note!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Department.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Department.Name.Contains(name))
                    .WhereIf(dateMin.HasValue, e => e.Department.Date >= dateMin!.Value)
                    .WhereIf(dateMax.HasValue, e => e.Department.Date <= dateMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Department.Note.Contains(note))
                    .WhereIf(titleId != null && titleId != Guid.Empty, e => e.Department.Titles.Any(x => x.TitleId == titleId));
        }

        public virtual async Task<List<Department>> GetListAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            string? note = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name, dateMin, dateMax, note);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DepartmentConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            string? note = null,
            Guid? titleId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, dateMin, dateMax, note, titleId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Department> ApplyFilter(
            IQueryable<Department> query,
            string? filterText = null,
            string? code = null,
            string? name = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            string? note = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code!.Contains(filterText!) || e.Name!.Contains(filterText!) || e.Note!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(dateMin.HasValue, e => e.Date >= dateMin!.Value)
                    .WhereIf(dateMax.HasValue, e => e.Date <= dateMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Note.Contains(note));
        }
    }
}