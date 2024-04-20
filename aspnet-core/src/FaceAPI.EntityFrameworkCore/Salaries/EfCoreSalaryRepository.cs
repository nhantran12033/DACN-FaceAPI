using FaceAPI.Titles;
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

namespace FaceAPI.Salaries
{
    public abstract class EfCoreSalaryRepositoryBase : EfCoreRepository<FaceAPIDbContext, Salary, Guid>
    {
        public EfCoreSalaryRepositoryBase(IDbContextProvider<FaceAPIDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<SalaryWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid departmentId, Guid titleId, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.DepartmentId == departmentId && b.TitleId == titleId)
                .Select(salary => new SalaryWithNavigationProperties
                {
                    Salary = salary,
                    Department = dbContext.Set<Department>().FirstOrDefault(c => c.Id == salary.DepartmentId),
                    Title = dbContext.Set<Title>().FirstOrDefault(c => c.Id == salary.TitleId)
                }).FirstOrDefault();
        }
        public virtual async Task<List<SalaryWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? code = null,
            double? allowanceMin = null,
            double? allowanceMax = null,
            double? basicMin = null,
            double? basicMax = null,
            double? bonusMin = null,
            double? bonusMax = null,
            double? totalMin = null,
            double? totalMax = null,
            Guid? departmentId = null,
            Guid? titleId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, allowanceMin, allowanceMax, basicMin, basicMax, bonusMin, bonusMax, totalMin, totalMax, departmentId, titleId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? SalaryConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<SalaryWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from salary in (await GetDbSetAsync())
                   join department in (await GetDbContextAsync()).Set<Department>() on salary.DepartmentId equals department.Id into departments
                   from department in departments.DefaultIfEmpty()
                   join title in (await GetDbContextAsync()).Set<Title>() on salary.TitleId equals title.Id into titles
                   from title in titles.DefaultIfEmpty()
                   select new SalaryWithNavigationProperties
                   {
                       Salary = salary,
                       Department = department,
                       Title = title
                   };
        }

        protected virtual IQueryable<SalaryWithNavigationProperties> ApplyFilter(
            IQueryable<SalaryWithNavigationProperties> query,
            string? filterText,
            string? code = null,
            double? allowanceMin = null,
            double? allowanceMax = null,
            double? basicMin = null,
            double? basicMax = null,
            double? bonusMin = null,
            double? bonusMax = null,
            double? totalMin = null,
            double? totalMax = null,
            Guid? departmentId = null,
            Guid? titleId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Salary.Code!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Salary.Code.Contains(code))
                    .WhereIf(allowanceMin.HasValue, e => e.Salary.Allowance >= allowanceMin!.Value)
                    .WhereIf(allowanceMax.HasValue, e => e.Salary.Allowance <= allowanceMax!.Value)
                    .WhereIf(basicMin.HasValue, e => e.Salary.Basic >= basicMin!.Value)
                    .WhereIf(basicMax.HasValue, e => e.Salary.Basic <= basicMax!.Value)
                    .WhereIf(bonusMin.HasValue, e => e.Salary.Bonus >= bonusMin!.Value)
                    .WhereIf(bonusMax.HasValue, e => e.Salary.Bonus <= bonusMax!.Value)
                    .WhereIf(totalMin.HasValue, e => e.Salary.Total >= totalMin!.Value)
                    .WhereIf(totalMax.HasValue, e => e.Salary.Total <= totalMax!.Value)
                    .WhereIf(departmentId != null && departmentId != Guid.Empty, e => e.Department != null && e.Department.Id == departmentId)
                    .WhereIf(titleId != null && titleId != Guid.Empty, e => e.Title != null && e.Title.Id == titleId);
        }

        public virtual async Task<List<Salary>> GetListAsync(
            string? filterText = null,
            string? code = null,
            double? allowanceMin = null,
            double? allowanceMax = null,
            double? basicMin = null,
            double? basicMax = null,
            double? bonusMin = null,
            double? bonusMax = null,
            double? totalMin = null,
            double? totalMax = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, allowanceMin, allowanceMax, basicMin, basicMax, bonusMin, bonusMax, totalMin, totalMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? SalaryConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? code = null,
            double? allowanceMin = null,
            double? allowanceMax = null,
            double? basicMin = null,
            double? basicMax = null,
            double? bonusMin = null,
            double? bonusMax = null,
            double? totalMin = null,
            double? totalMax = null,
            Guid? departmentId = null,
            Guid? titleId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, allowanceMin, allowanceMax, basicMin, basicMax, bonusMin, bonusMax, totalMin, totalMax, departmentId, titleId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Salary> ApplyFilter(
            IQueryable<Salary> query,
            string? filterText = null,
            string? code = null,
            double? allowanceMin = null,
            double? allowanceMax = null,
            double? basicMin = null,
            double? basicMax = null,
            double? bonusMin = null,
            double? bonusMax = null,
            double? totalMin = null,
            double? totalMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(allowanceMin.HasValue, e => e.Allowance >= allowanceMin!.Value)
                    .WhereIf(allowanceMax.HasValue, e => e.Allowance <= allowanceMax!.Value)
                    .WhereIf(basicMin.HasValue, e => e.Basic >= basicMin!.Value)
                    .WhereIf(basicMax.HasValue, e => e.Basic <= basicMax!.Value)
                    .WhereIf(bonusMin.HasValue, e => e.Bonus >= bonusMin!.Value)
                    .WhereIf(bonusMax.HasValue, e => e.Bonus <= bonusMax!.Value)
                    .WhereIf(totalMin.HasValue, e => e.Total >= totalMin!.Value)
                    .WhereIf(totalMax.HasValue, e => e.Total <= totalMax!.Value);
        }
    }
}