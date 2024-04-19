using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace FaceAPI.Timesheets
{
    public abstract class TimesheetManagerBase : DomainService
    {
        protected ITimesheetRepository _timesheetRepository;

        public TimesheetManagerBase(ITimesheetRepository timesheetRepository)
        {
            _timesheetRepository = timesheetRepository;
        }

        public virtual async Task<Timesheet> CreateAsync(
        Guid? scheduleId, Guid? scheduleDetailId, Guid? scheduleFormatId, DateTime time, string? image = null, string? code = null, string? note = null)
        {
            Check.NotNull(time, nameof(time));

            var timesheet = new Timesheet(
             GuidGenerator.Create(),
             scheduleId, scheduleDetailId, scheduleFormatId, time, image, code, note
             );

            return await _timesheetRepository.InsertAsync(timesheet);
        }

        public virtual async Task<Timesheet> UpdateAsync(
            Guid id,
            Guid? scheduleId, Guid? scheduleDetailId, Guid? scheduleFormatId, DateTime time, string? image = null, string? code = null, string? note = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(time, nameof(time));

            var timesheet = await _timesheetRepository.GetAsync(id);

            timesheet.ScheduleId = scheduleId;
            timesheet.ScheduleDetailId = scheduleDetailId;
            timesheet.ScheduleFormatId = scheduleFormatId;
            timesheet.Time = time;
            timesheet.Image = image;
            timesheet.Code = code;
            timesheet.Note = note;

            timesheet.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _timesheetRepository.UpdateAsync(timesheet);
        }

    }
}