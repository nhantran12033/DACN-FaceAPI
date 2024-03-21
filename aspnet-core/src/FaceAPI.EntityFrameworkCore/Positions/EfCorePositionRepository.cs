using FaceAPI.Departments;
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

namespace FaceAPI.Positions
{
    public abstract class EfCorePositionRepositoryBase : EfCoreRepository<FaceAPIDbContext, Position, Guid>
    {
        public EfCorePositionRepositoryBase(IDbContextProvider<FaceAPIDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<PositionWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(position => new PositionWithNavigationProperties
                {
                    Position = position,
                    Department = dbContext.Set<Department>().FirstOrDefault(c => c.Id == position.DepartmentId)
                }).FirstOrDefault();
        }

        public virtual async Task<List<PositionWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            string? note = null,
            Guid? departmentId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, note, departmentId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PositionConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<PositionWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from position in (await GetDbSetAsync())
                   join department in (await GetDbContextAsync()).Set<Department>() on position.DepartmentId equals department.Id into departments
                   from department in departments.DefaultIfEmpty()
                   select new PositionWithNavigationProperties
                   {
                       Position = position,
                       Department = department
                   };
        }

        protected virtual IQueryable<PositionWithNavigationProperties> ApplyFilter(
            IQueryable<PositionWithNavigationProperties> query,
            string? filterText,
            string? code = null,
            string? name = null,
            string? note = null,
            Guid? departmentId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Position.Code!.Contains(filterText!) || e.Position.Name!.Contains(filterText!) || e.Position.Note!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Position.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Position.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Position.Note.Contains(note))
                    .WhereIf(departmentId != null && departmentId != Guid.Empty, e => e.Department != null && e.Department.Id == departmentId);
        }

        public virtual async Task<List<Position>> GetListAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            string? note = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name, note);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PositionConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            string? note = null,
            Guid? departmentId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, note, departmentId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Position> ApplyFilter(
            IQueryable<Position> query,
            string? filterText = null,
            string? code = null,
            string? name = null,
            string? note = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code!.Contains(filterText!) || e.Name!.Contains(filterText!) || e.Note!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Note.Contains(note));
        }
    }
}