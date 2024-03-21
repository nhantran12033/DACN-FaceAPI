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

namespace FaceAPI.ScheduleDetails
{
    public abstract class EfCoreScheduleDetailRepositoryBase : EfCoreRepository<FaceAPIDbContext, ScheduleDetail, Guid>
    {
        public EfCoreScheduleDetailRepositoryBase(IDbContextProvider<FaceAPIDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<ScheduleDetail>> GetListAsync(
            string? filterText = null,
            string? name = null,
            DateTime? fromMin = null,
            DateTime? fromMax = null,
            DateTime? toMin = null,
            DateTime? toMax = null,
            string? note = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, fromMin, fromMax, toMin, toMax, note);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ScheduleDetailConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            DateTime? fromMin = null,
            DateTime? fromMax = null,
            DateTime? toMin = null,
            DateTime? toMax = null,
            string? note = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, fromMin, fromMax, toMin, toMax, note);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ScheduleDetail> ApplyFilter(
            IQueryable<ScheduleDetail> query,
            string? filterText = null,
            string? name = null,
            DateTime? fromMin = null,
            DateTime? fromMax = null,
            DateTime? toMin = null,
            DateTime? toMax = null,
            string? note = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.Note!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(fromMin.HasValue, e => e.From >= fromMin!.Value)
                    .WhereIf(fromMax.HasValue, e => e.From <= fromMax!.Value)
                    .WhereIf(toMin.HasValue, e => e.To >= toMin!.Value)
                    .WhereIf(toMax.HasValue, e => e.To <= toMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Note.Contains(note));
        }
    }
}