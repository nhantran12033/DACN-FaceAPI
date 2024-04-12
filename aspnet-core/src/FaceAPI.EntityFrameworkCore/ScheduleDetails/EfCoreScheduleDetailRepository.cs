using FaceAPI.ScheduleFormats;
using FaceAPI.ScheduleFormats;
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

        public virtual async Task<ScheduleDetailWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id).Include(x => x.ScheduleFormats)
                .Select(scheduleDetail => new ScheduleDetailWithNavigationProperties
                {
                    ScheduleDetail = scheduleDetail,
                    ScheduleFormats = (from scheduleDetailScheduleFormats in scheduleDetail.ScheduleFormats
                                       join _scheduleFormat in dbContext.Set<ScheduleFormat>() on scheduleDetailScheduleFormats.ScheduleFormatId equals _scheduleFormat.Id
                                       select _scheduleFormat).ToList()
                }).FirstOrDefault();
        }

        public virtual async Task<List<ScheduleDetailWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? name = null,
            DateTime? fromDateMin = null,
            DateTime? fromDateMax = null,
            DateTime? toDateMin = null,
            DateTime? toDateMax = null,
            string? note = null,
            Guid? scheduleFormatId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, fromDateMin, fromDateMax, toDateMin, toDateMax, note, scheduleFormatId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ScheduleDetailConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ScheduleDetailWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from scheduleDetail in (await GetDbSetAsync())

                   select new ScheduleDetailWithNavigationProperties
                   {
                       ScheduleDetail = scheduleDetail,
                       ScheduleFormats = new List<ScheduleFormat>()
                   };
        }

        protected virtual IQueryable<ScheduleDetailWithNavigationProperties> ApplyFilter(
            IQueryable<ScheduleDetailWithNavigationProperties> query,
            string? filterText,
            string? name = null,
            DateTime? fromDateMin = null,
            DateTime? fromDateMax = null,
            DateTime? toDateMin = null,
            DateTime? toDateMax = null,
            string? note = null,
            Guid? scheduleFormatId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ScheduleDetail.Name!.Contains(filterText!) || e.ScheduleDetail.Note!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.ScheduleDetail.Name.Contains(name))
                    .WhereIf(fromDateMin.HasValue, e => e.ScheduleDetail.FromDate >= fromDateMin!.Value)
                    .WhereIf(fromDateMax.HasValue, e => e.ScheduleDetail.FromDate <= fromDateMax!.Value)
                    .WhereIf(toDateMin.HasValue, e => e.ScheduleDetail.ToDate >= toDateMin!.Value)
                    .WhereIf(toDateMax.HasValue, e => e.ScheduleDetail.ToDate <= toDateMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.ScheduleDetail.Note.Contains(note))
                    .WhereIf(scheduleFormatId != null && scheduleFormatId != Guid.Empty, e => e.ScheduleDetail.ScheduleFormats.Any(x => x.ScheduleFormatId == scheduleFormatId));
        }

        public virtual async Task<List<ScheduleDetail>> GetListAsync(
            string? filterText = null,
            string? name = null,
            DateTime? fromDateMin = null,
            DateTime? fromDateMax = null,
            DateTime? toDateMin = null,
            DateTime? toDateMax = null,
            string? note = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, fromDateMin, fromDateMax, toDateMin, toDateMax, note);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ScheduleDetailConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            DateTime? fromDateMin = null,
            DateTime? fromDateMax = null,
            DateTime? toDateMin = null,
            DateTime? toDateMax = null,
            string? note = null,
            Guid? scheduleFormatId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, fromDateMin, fromDateMax, toDateMin, toDateMax, note, scheduleFormatId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ScheduleDetail> ApplyFilter(
            IQueryable<ScheduleDetail> query,
            string? filterText = null,
            string? name = null,
            DateTime? fromDateMin = null,
            DateTime? fromDateMax = null,
            DateTime? toDateMin = null,
            DateTime? toDateMax = null,
            string? note = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.Note!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(fromDateMin.HasValue, e => e.FromDate >= fromDateMin!.Value)
                    .WhereIf(fromDateMax.HasValue, e => e.FromDate <= fromDateMax!.Value)
                    .WhereIf(toDateMin.HasValue, e => e.ToDate >= toDateMin!.Value)
                    .WhereIf(toDateMax.HasValue, e => e.ToDate <= toDateMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Note.Contains(note));
        }
    }
}