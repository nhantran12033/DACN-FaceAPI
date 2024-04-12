using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace FaceAPI.ScheduleFormats
{
    public abstract class ScheduleFormatManagerBase : DomainService
    {
        protected IScheduleFormatRepository _scheduleFormatRepository;

        public ScheduleFormatManagerBase(IScheduleFormatRepository scheduleFormatRepository)
        {
            _scheduleFormatRepository = scheduleFormatRepository;
        }

        public virtual async Task<ScheduleFormat> CreateAsync(
        DateTime date, int fromHours, int toHours, string? name = null, string? note = null)
        {
            Check.NotNull(date, nameof(date));

            var scheduleFormat = new ScheduleFormat(
             GuidGenerator.Create(),
             date, fromHours, toHours, name, note
             );

            return await _scheduleFormatRepository.InsertAsync(scheduleFormat);
        }

        public virtual async Task<ScheduleFormat> UpdateAsync(
            Guid id,
            DateTime date, int fromHours, int toHours, string? name = null, string? note = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(date, nameof(date));

            var scheduleFormat = await _scheduleFormatRepository.GetAsync(id);

            scheduleFormat.Date = date;
            scheduleFormat.FromHours = fromHours;
            scheduleFormat.ToHours = toHours;
            scheduleFormat.Name = name;
            scheduleFormat.Note = note;

            scheduleFormat.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _scheduleFormatRepository.UpdateAsync(scheduleFormat);
        }

    }
}