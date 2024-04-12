using FaceAPI.ScheduleFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace FaceAPI.ScheduleDetails
{
    public abstract class ScheduleDetailManagerBase : DomainService
    {
        protected IScheduleDetailRepository _scheduleDetailRepository;
        protected IRepository<ScheduleFormat, Guid> _scheduleFormatRepository;

        public ScheduleDetailManagerBase(IScheduleDetailRepository scheduleDetailRepository,
        IRepository<ScheduleFormat, Guid> scheduleFormatRepository)
        {
            _scheduleDetailRepository = scheduleDetailRepository;
            _scheduleFormatRepository = scheduleFormatRepository;
        }

        public virtual async Task<ScheduleDetail> CreateAsync(
        List<Guid> scheduleFormatIds,
        DateTime fromDate, DateTime toDate, string? name = null, string? note = null)
        {
            Check.NotNull(fromDate, nameof(fromDate));
            Check.NotNull(toDate, nameof(toDate));

            var scheduleDetail = new ScheduleDetail(
             GuidGenerator.Create(),
             fromDate, toDate, name, note
             );

            await SetScheduleFormatsAsync(scheduleDetail, scheduleFormatIds);

            return await _scheduleDetailRepository.InsertAsync(scheduleDetail);
        }

        public virtual async Task<ScheduleDetail> UpdateAsync(
            Guid id,
            List<Guid> scheduleFormatIds,
        DateTime fromDate, DateTime toDate, string? name = null, string? note = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(fromDate, nameof(fromDate));
            Check.NotNull(toDate, nameof(toDate));

            var queryable = await _scheduleDetailRepository.WithDetailsAsync(x => x.ScheduleFormats);
            var query = queryable.Where(x => x.Id == id);

            var scheduleDetail = await AsyncExecuter.FirstOrDefaultAsync(query);

            scheduleDetail.FromDate = fromDate;
            scheduleDetail.ToDate = toDate;
            scheduleDetail.Name = name;
            scheduleDetail.Note = note;

            await SetScheduleFormatsAsync(scheduleDetail, scheduleFormatIds);

            scheduleDetail.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _scheduleDetailRepository.UpdateAsync(scheduleDetail);
        }

        private async Task SetScheduleFormatsAsync(ScheduleDetail scheduleDetail, List<Guid> scheduleFormatIds)
        {
            if (scheduleFormatIds == null || !scheduleFormatIds.Any())
            {
                scheduleDetail.RemoveAllScheduleFormats();
                return;
            }

            var query = (await _scheduleFormatRepository.GetQueryableAsync())
                .Where(x => scheduleFormatIds.Contains(x.Id))
                .Select(x => x.Id);

            var scheduleFormatIdsInDb = await AsyncExecuter.ToListAsync(query);
            if (!scheduleFormatIdsInDb.Any())
            {
                return;
            }

            scheduleDetail.RemoveAllScheduleFormatsExceptGivenIds(scheduleFormatIdsInDb);

            foreach (var scheduleFormatId in scheduleFormatIdsInDb)
            {
                scheduleDetail.AddScheduleFormat(scheduleFormatId);
            }
        }

    }
}