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
        Guid? scheduleId, Guid? scheduleDetailId, Guid? scheduleFormatId, DateTime timeIn, DateTime timeOut, int hoursWork, string? code = null, string? note = null)
        {
            Check.NotNull(timeIn, nameof(timeIn));
            Check.NotNull(timeOut, nameof(timeOut));

            var timesheet = new Timesheet(
             GuidGenerator.Create(),
             scheduleId, scheduleDetailId, scheduleFormatId, timeIn, timeOut, hoursWork, code, note
             );

            return await _timesheetRepository.InsertAsync(timesheet);
        }

        public virtual async Task<Timesheet> UpdateAsync(
            Guid id,
            Guid? scheduleId, Guid? scheduleDetailId, Guid? scheduleFormatId, DateTime timeIn, DateTime timeOut, int hoursWork, string? code = null, string? note = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(timeIn, nameof(timeIn));
            Check.NotNull(timeOut, nameof(timeOut));

            var timesheet = await _timesheetRepository.GetAsync(id);

            timesheet.ScheduleId = scheduleId;
            timesheet.ScheduleDetailId = scheduleDetailId;
            timesheet.ScheduleFormatId = scheduleFormatId;
            timesheet.TimeIn = timeIn;
            timesheet.TimeOut = timeOut;
            timesheet.HoursWork = hoursWork;
            timesheet.Code = code;
            timesheet.Note = note;

            timesheet.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _timesheetRepository.UpdateAsync(timesheet);
        }

    }
}