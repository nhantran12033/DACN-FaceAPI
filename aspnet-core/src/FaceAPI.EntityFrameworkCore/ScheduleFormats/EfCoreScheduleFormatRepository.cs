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

namespace FaceAPI.ScheduleFormats
{
    public abstract class EfCoreScheduleFormatRepositoryBase : EfCoreRepository<FaceAPIDbContext, ScheduleFormat, Guid>
    {
        public EfCoreScheduleFormatRepositoryBase(IDbContextProvider<FaceAPIDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<ScheduleFormat>> GetListAsync(
            string? filterText = null,
            string? name = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            int? fromHoursMin = null,
            int? fromHoursMax = null,
            int? toHoursMin = null,
            int? toHoursMax = null,
            string? note = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, dateMin, dateMax, fromHoursMin, fromHoursMax, toHoursMin, toHoursMax, note);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ScheduleFormatConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            int? fromHoursMin = null,
            int? fromHoursMax = null,
            int? toHoursMin = null,
            int? toHoursMax = null,
            string? note = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, dateMin, dateMax, fromHoursMin, fromHoursMax, toHoursMin, toHoursMax, note);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ScheduleFormat> ApplyFilter(
            IQueryable<ScheduleFormat> query,
            string? filterText = null,
            string? name = null,
            DateTime? dateMin = null,
            DateTime? dateMax = null,
            int? fromHoursMin = null,
            int? fromHoursMax = null,
            int? toHoursMin = null,
            int? toHoursMax = null,
            string? note = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.Note!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(dateMin.HasValue, e => e.Date >= dateMin!.Value)
                    .WhereIf(dateMax.HasValue, e => e.Date <= dateMax!.Value)
                    .WhereIf(fromHoursMin.HasValue, e => e.FromHours >= fromHoursMin!.Value)
                    .WhereIf(fromHoursMax.HasValue, e => e.FromHours <= fromHoursMax!.Value)
                    .WhereIf(toHoursMin.HasValue, e => e.ToHours >= toHoursMin!.Value)
                    .WhereIf(toHoursMax.HasValue, e => e.ToHours <= toHoursMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Note.Contains(note));
        }
    }
}