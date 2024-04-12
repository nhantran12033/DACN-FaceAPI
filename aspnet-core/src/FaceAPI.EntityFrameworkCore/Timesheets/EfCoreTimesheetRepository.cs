using FaceAPI.ScheduleFormats;
using FaceAPI.ScheduleDetails;
using FaceAPI.Schedules;
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

namespace FaceAPI.Timesheets
{
    public abstract class EfCoreTimesheetRepositoryBase : EfCoreRepository<FaceAPIDbContext, Timesheet, Guid>
    {
        public EfCoreTimesheetRepositoryBase(IDbContextProvider<FaceAPIDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<TimesheetWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(timesheet => new TimesheetWithNavigationProperties
                {
                    Timesheet = timesheet,
                    Schedule = dbContext.Set<Schedule>().FirstOrDefault(c => c.Id == timesheet.ScheduleId),
                    ScheduleDetail = dbContext.Set<ScheduleDetail>().FirstOrDefault(c => c.Id == timesheet.ScheduleDetailId),
                    ScheduleFormat = dbContext.Set<ScheduleFormat>().FirstOrDefault(c => c.Id == timesheet.ScheduleFormatId)
                }).FirstOrDefault();
        }

        public virtual async Task<List<TimesheetWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? code = null,
            DateTime? timeInMin = null,
            DateTime? timeInMax = null,
            DateTime? timeOutMin = null,
            DateTime? timeOutMax = null,
            int? hoursWorkMin = null,
            int? hoursWorkMax = null,
            string? note = null,
            Guid? scheduleId = null,
            Guid? scheduleDetailId = null,
            Guid? scheduleFormatId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, timeInMin, timeInMax, timeOutMin, timeOutMax, hoursWorkMin, hoursWorkMax, note, scheduleId, scheduleDetailId, scheduleFormatId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TimesheetConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<TimesheetWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from timesheet in (await GetDbSetAsync())
                   join schedule in (await GetDbContextAsync()).Set<Schedule>() on timesheet.ScheduleId equals schedule.Id into schedules
                   from schedule in schedules.DefaultIfEmpty()
                   join scheduleDetail in (await GetDbContextAsync()).Set<ScheduleDetail>() on timesheet.ScheduleDetailId equals scheduleDetail.Id into scheduleDetails
                   from scheduleDetail in scheduleDetails.DefaultIfEmpty()
                   join scheduleFormat in (await GetDbContextAsync()).Set<ScheduleFormat>() on timesheet.ScheduleFormatId equals scheduleFormat.Id into scheduleFormats
                   from scheduleFormat in scheduleFormats.DefaultIfEmpty()
                   select new TimesheetWithNavigationProperties
                   {
                       Timesheet = timesheet,
                       Schedule = schedule,
                       ScheduleDetail = scheduleDetail,
                       ScheduleFormat = scheduleFormat
                   };
        }

        protected virtual IQueryable<TimesheetWithNavigationProperties> ApplyFilter(
            IQueryable<TimesheetWithNavigationProperties> query,
            string? filterText,
            string? code = null,
            DateTime? timeInMin = null,
            DateTime? timeInMax = null,
            DateTime? timeOutMin = null,
            DateTime? timeOutMax = null,
            int? hoursWorkMin = null,
            int? hoursWorkMax = null,
            string? note = null,
            Guid? scheduleId = null,
            Guid? scheduleDetailId = null,
            Guid? scheduleFormatId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Timesheet.Code!.Contains(filterText!) || e.Timesheet.Note!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Timesheet.Code.Contains(code))
                    .WhereIf(timeInMin.HasValue, e => e.Timesheet.TimeIn >= timeInMin!.Value)
                    .WhereIf(timeInMax.HasValue, e => e.Timesheet.TimeIn <= timeInMax!.Value)
                    .WhereIf(timeOutMin.HasValue, e => e.Timesheet.TimeOut >= timeOutMin!.Value)
                    .WhereIf(timeOutMax.HasValue, e => e.Timesheet.TimeOut <= timeOutMax!.Value)
                    .WhereIf(hoursWorkMin.HasValue, e => e.Timesheet.HoursWork >= hoursWorkMin!.Value)
                    .WhereIf(hoursWorkMax.HasValue, e => e.Timesheet.HoursWork <= hoursWorkMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Timesheet.Note.Contains(note))
                    .WhereIf(scheduleId != null && scheduleId != Guid.Empty, e => e.Schedule != null && e.Schedule.Id == scheduleId)
                    .WhereIf(scheduleDetailId != null && scheduleDetailId != Guid.Empty, e => e.ScheduleDetail != null && e.ScheduleDetail.Id == scheduleDetailId)
                    .WhereIf(scheduleFormatId != null && scheduleFormatId != Guid.Empty, e => e.ScheduleFormat != null && e.ScheduleFormat.Id == scheduleFormatId);
        }

        public virtual async Task<List<Timesheet>> GetListAsync(
            string? filterText = null,
            string? code = null,
            DateTime? timeInMin = null,
            DateTime? timeInMax = null,
            DateTime? timeOutMin = null,
            DateTime? timeOutMax = null,
            int? hoursWorkMin = null,
            int? hoursWorkMax = null,
            string? note = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, timeInMin, timeInMax, timeOutMin, timeOutMax, hoursWorkMin, hoursWorkMax, note);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TimesheetConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? code = null,
            DateTime? timeInMin = null,
            DateTime? timeInMax = null,
            DateTime? timeOutMin = null,
            DateTime? timeOutMax = null,
            int? hoursWorkMin = null,
            int? hoursWorkMax = null,
            string? note = null,
            Guid? scheduleId = null,
            Guid? scheduleDetailId = null,
            Guid? scheduleFormatId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, timeInMin, timeInMax, timeOutMin, timeOutMax, hoursWorkMin, hoursWorkMax, note, scheduleId, scheduleDetailId, scheduleFormatId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Timesheet> ApplyFilter(
            IQueryable<Timesheet> query,
            string? filterText = null,
            string? code = null,
            DateTime? timeInMin = null,
            DateTime? timeInMax = null,
            DateTime? timeOutMin = null,
            DateTime? timeOutMax = null,
            int? hoursWorkMin = null,
            int? hoursWorkMax = null,
            string? note = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code!.Contains(filterText!) || e.Note!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(timeInMin.HasValue, e => e.TimeIn >= timeInMin!.Value)
                    .WhereIf(timeInMax.HasValue, e => e.TimeIn <= timeInMax!.Value)
                    .WhereIf(timeOutMin.HasValue, e => e.TimeOut >= timeOutMin!.Value)
                    .WhereIf(timeOutMax.HasValue, e => e.TimeOut <= timeOutMax!.Value)
                    .WhereIf(hoursWorkMin.HasValue, e => e.HoursWork >= hoursWorkMin!.Value)
                    .WhereIf(hoursWorkMax.HasValue, e => e.HoursWork <= hoursWorkMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Note.Contains(note));
        }
    }
}