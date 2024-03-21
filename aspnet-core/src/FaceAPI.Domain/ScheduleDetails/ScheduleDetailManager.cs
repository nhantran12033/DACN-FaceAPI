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

        public ScheduleDetailManagerBase(IScheduleDetailRepository scheduleDetailRepository)
        {
            _scheduleDetailRepository = scheduleDetailRepository;
        }

        public virtual async Task<ScheduleDetail> CreateAsync(
        DateTime from, DateTime to, string? name = null, string? note = null)
        {
            Check.NotNull(from, nameof(from));
            Check.NotNull(to, nameof(to));

            var scheduleDetail = new ScheduleDetail(
             GuidGenerator.Create(),
             from, to, name, note
             );

            return await _scheduleDetailRepository.InsertAsync(scheduleDetail);
        }

        public virtual async Task<ScheduleDetail> UpdateAsync(
            Guid id,
            DateTime from, DateTime to, string? name = null, string? note = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(from, nameof(from));
            Check.NotNull(to, nameof(to));

            var scheduleDetail = await _scheduleDetailRepository.GetAsync(id);

            scheduleDetail.From = from;
            scheduleDetail.To = to;
            scheduleDetail.Name = name;
            scheduleDetail.Note = note;

            scheduleDetail.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _scheduleDetailRepository.UpdateAsync(scheduleDetail);
        }

    }
}