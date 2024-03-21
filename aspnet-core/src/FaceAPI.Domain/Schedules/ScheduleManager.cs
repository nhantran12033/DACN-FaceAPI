using FaceAPI.ScheduleDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace FaceAPI.Schedules
{
    public abstract class ScheduleManagerBase : DomainService
    {
        protected IScheduleRepository _scheduleRepository;
        protected IRepository<ScheduleDetail, Guid> _scheduleDetailRepository;

        public ScheduleManagerBase(IScheduleRepository scheduleRepository,
        IRepository<ScheduleDetail, Guid> scheduleDetailRepository)
        {
            _scheduleRepository = scheduleRepository;
            _scheduleDetailRepository = scheduleDetailRepository;
        }

        public virtual async Task<Schedule> CreateAsync(
        List<Guid> scheduleDetailIds,
        Guid? departmentId, DateTime dateFrom, DateTime dateTo, string? code = null, string? name = null, string? note = null)
        {
            Check.NotNull(dateFrom, nameof(dateFrom));
            Check.NotNull(dateTo, nameof(dateTo));

            var schedule = new Schedule(
             GuidGenerator.Create(),
             departmentId, dateFrom, dateTo, code, name, note
             );

            await SetScheduleDetailsAsync(schedule, scheduleDetailIds);

            return await _scheduleRepository.InsertAsync(schedule);
        }

        public virtual async Task<Schedule> UpdateAsync(
            Guid id,
            List<Guid> scheduleDetailIds,
        Guid? departmentId, DateTime dateFrom, DateTime dateTo, string? code = null, string? name = null, string? note = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(dateFrom, nameof(dateFrom));
            Check.NotNull(dateTo, nameof(dateTo));

            var queryable = await _scheduleRepository.WithDetailsAsync(x => x.ScheduleDetails);
            var query = queryable.Where(x => x.Id == id);

            var schedule = await AsyncExecuter.FirstOrDefaultAsync(query);

            schedule.DepartmentId = departmentId;
            schedule.DateFrom = dateFrom;
            schedule.DateTo = dateTo;
            schedule.Code = code;
            schedule.Name = name;
            schedule.Note = note;

            await SetScheduleDetailsAsync(schedule, scheduleDetailIds);

            schedule.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _scheduleRepository.UpdateAsync(schedule);
        }

        private async Task SetScheduleDetailsAsync(Schedule schedule, List<Guid> scheduleDetailIds)
        {
            if (scheduleDetailIds == null || !scheduleDetailIds.Any())
            {
                schedule.RemoveAllScheduleDetails();
                return;
            }

            var query = (await _scheduleDetailRepository.GetQueryableAsync())
                .Where(x => scheduleDetailIds.Contains(x.Id))
                .Select(x => x.Id);

            var scheduleDetailIdsInDb = await AsyncExecuter.ToListAsync(query);
            if (!scheduleDetailIdsInDb.Any())
            {
                return;
            }

            schedule.RemoveAllScheduleDetailsExceptGivenIds(scheduleDetailIdsInDb);

            foreach (var scheduleDetailId in scheduleDetailIdsInDb)
            {
                schedule.AddScheduleDetail(scheduleDetailId);
            }
        }

    }
}