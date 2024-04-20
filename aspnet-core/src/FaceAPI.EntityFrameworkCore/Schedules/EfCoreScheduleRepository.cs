using FaceAPI.Staffs;
using FaceAPI.ScheduleDetails;
using FaceAPI.ScheduleDetails;
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

namespace FaceAPI.Schedules
{
    public abstract class EfCoreScheduleRepositoryBase : EfCoreRepository<FaceAPIDbContext, Schedule, Guid>
    {
        public EfCoreScheduleRepositoryBase(IDbContextProvider<FaceAPIDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<ScheduleWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id).Include(x => x.ScheduleDetails)
                .Select(schedule => new ScheduleWithNavigationProperties
                {
                    Schedule = schedule,
                    Staff = dbContext.Set<Staff>().FirstOrDefault(c => c.Id == schedule.StaffId),
                    ScheduleDetails = (from scheduleScheduleDetails in schedule.ScheduleDetails
                                       join _scheduleDetail in dbContext.Set<ScheduleDetail>() on scheduleScheduleDetails.ScheduleDetailId equals _scheduleDetail.Id
                                       select _scheduleDetail).ToList()
                }).FirstOrDefault();
        }
        public virtual async Task<List<ScheduleWithNavigationProperties>> GetWithCodeNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.StaffId == id).Include(x => x.ScheduleDetails)
                .Select(schedule => new ScheduleWithNavigationProperties
                {
                    Schedule = schedule,
                    Staff = dbContext.Set<Staff>().FirstOrDefault(c => c.Id == schedule.StaffId),
                    ScheduleDetails = (from scheduleScheduleDetails in schedule.ScheduleDetails
                                       join _scheduleDetail in dbContext.Set<ScheduleDetail>() on scheduleScheduleDetails.ScheduleDetailId equals _scheduleDetail.Id
                                       select _scheduleDetail).ToList()
                }).ToList();
        }
        public virtual async Task<List<ScheduleWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            DateTime? dateFromMin = null,
            DateTime? dateFromMax = null,
            DateTime? dateToMin = null,
            DateTime? dateToMax = null,
            string? note = null,
            Guid? staffId = null,
            Guid? scheduleDetailId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, dateFromMin, dateFromMax, dateToMin, dateToMax, note, staffId, scheduleDetailId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ScheduleConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ScheduleWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from schedule in (await GetDbSetAsync())
                   join staff in (await GetDbContextAsync()).Set<Staff>() on schedule.StaffId equals staff.Id into staffs
                   from staff in staffs.DefaultIfEmpty()
                   select new ScheduleWithNavigationProperties
                   {
                       Schedule = schedule,
                       Staff = staff,
                       ScheduleDetails = new List<ScheduleDetail>()
                   };
        }

        protected virtual IQueryable<ScheduleWithNavigationProperties> ApplyFilter(
            IQueryable<ScheduleWithNavigationProperties> query,
            string? filterText,
            string? code = null,
            string? name = null,
            DateTime? dateFromMin = null,
            DateTime? dateFromMax = null,
            DateTime? dateToMin = null,
            DateTime? dateToMax = null,
            string? note = null,
            Guid? staffId = null,
            Guid? scheduleDetailId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Schedule.Code!.Contains(filterText!) || e.Schedule.Name!.Contains(filterText!) || e.Schedule.Note!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Schedule.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Schedule.Name.Contains(name))
                    .WhereIf(dateFromMin.HasValue, e => e.Schedule.DateFrom >= dateFromMin!.Value)
                    .WhereIf(dateFromMax.HasValue, e => e.Schedule.DateFrom <= dateFromMax!.Value)
                    .WhereIf(dateToMin.HasValue, e => e.Schedule.DateTo >= dateToMin!.Value)
                    .WhereIf(dateToMax.HasValue, e => e.Schedule.DateTo <= dateToMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Schedule.Note.Contains(note))
                    .WhereIf(staffId != null && staffId != Guid.Empty, e => e.Staff != null && e.Staff.Id == staffId)
                    .WhereIf(scheduleDetailId != null && scheduleDetailId != Guid.Empty, e => e.Schedule.ScheduleDetails.Any(x => x.ScheduleDetailId == scheduleDetailId));
        }

        public virtual async Task<List<Schedule>> GetListAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            DateTime? dateFromMin = null,
            DateTime? dateFromMax = null,
            DateTime? dateToMin = null,
            DateTime? dateToMax = null,
            string? note = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name, dateFromMin, dateFromMax, dateToMin, dateToMax, note);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ScheduleConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            DateTime? dateFromMin = null,
            DateTime? dateFromMax = null,
            DateTime? dateToMin = null,
            DateTime? dateToMax = null,
            string? note = null,
            Guid? staffId = null,
            Guid? scheduleDetailId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, dateFromMin, dateFromMax, dateToMin, dateToMax, note, staffId, scheduleDetailId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Schedule> ApplyFilter(
            IQueryable<Schedule> query,
            string? filterText = null,
            string? code = null,
            string? name = null,
            DateTime? dateFromMin = null,
            DateTime? dateFromMax = null,
            DateTime? dateToMin = null,
            DateTime? dateToMax = null,
            string? note = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code!.Contains(filterText!) || e.Name!.Contains(filterText!) || e.Note!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(dateFromMin.HasValue, e => e.DateFrom >= dateFromMin!.Value)
                    .WhereIf(dateFromMax.HasValue, e => e.DateFrom <= dateFromMax!.Value)
                    .WhereIf(dateToMin.HasValue, e => e.DateTo >= dateToMin!.Value)
                    .WhereIf(dateToMax.HasValue, e => e.DateTo <= dateToMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Note.Contains(note));
        }
    }
}