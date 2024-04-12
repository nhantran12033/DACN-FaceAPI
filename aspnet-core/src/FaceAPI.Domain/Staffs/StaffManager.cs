using FaceAPI.Timesheets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace FaceAPI.Staffs
{
    public abstract class StaffManagerBase : DomainService
    {
        protected IStaffRepository _staffRepository;
        protected IRepository<Timesheet, Guid> _timesheetRepository;

        public StaffManagerBase(IStaffRepository staffRepository,
        IRepository<Timesheet, Guid> timesheetRepository)
        {
            _staffRepository = staffRepository;
            _timesheetRepository = timesheetRepository;
        }

        public virtual async Task<Staff> CreateAsync(
        List<Guid> timesheetIds,
        Guid? departmentId, Guid? titleId, DateTime birthday, DateTime startWork, int debt, string? image = null, string? code = null, string? name = null, string? sex = null, string? phone = null, string? email = null, string? address = null, string? note = null)
        {
            Check.NotNull(birthday, nameof(birthday));
            Check.NotNull(startWork, nameof(startWork));

            var staff = new Staff(
             GuidGenerator.Create(),
             departmentId, titleId, birthday, startWork, debt, image, code, name, sex, phone, email, address, note
             );

            await SetTimesheetsAsync(staff, timesheetIds);

            return await _staffRepository.InsertAsync(staff);
        }

        public virtual async Task<Staff> UpdateAsync(
            Guid id,
            List<Guid> timesheetIds,
        Guid? departmentId, Guid? titleId, DateTime birthday, DateTime startWork, int debt, string? image = null, string? code = null, string? name = null, string? sex = null, string? phone = null, string? email = null, string? address = null, string? note = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(birthday, nameof(birthday));
            Check.NotNull(startWork, nameof(startWork));

            var queryable = await _staffRepository.WithDetailsAsync(x => x.Timesheets);
            var query = queryable.Where(x => x.Id == id);

            var staff = await AsyncExecuter.FirstOrDefaultAsync(query);

            staff.DepartmentId = departmentId;
            staff.TitleId = titleId;
            staff.Birthday = birthday;
            staff.StartWork = startWork;
            staff.Debt = debt;
            staff.Image = image;
            staff.Code = code;
            staff.Name = name;
            staff.Sex = sex;
            staff.Phone = phone;
            staff.Email = email;
            staff.Address = address;
            staff.Note = note;

            await SetTimesheetsAsync(staff, timesheetIds);

            staff.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _staffRepository.UpdateAsync(staff);
        }

        private async Task SetTimesheetsAsync(Staff staff, List<Guid> timesheetIds)
        {
            if (timesheetIds == null || !timesheetIds.Any())
            {
                staff.RemoveAllTimesheets();
                return;
            }

            var query = (await _timesheetRepository.GetQueryableAsync())
                .Where(x => timesheetIds.Contains(x.Id))
                .Select(x => x.Id);

            var timesheetIdsInDb = await AsyncExecuter.ToListAsync(query);
            if (!timesheetIdsInDb.Any())
            {
                return;
            }

            staff.RemoveAllTimesheetsExceptGivenIds(timesheetIdsInDb);

            foreach (var timesheetId in timesheetIdsInDb)
            {
                staff.AddTimesheet(timesheetId);
            }
        }

    }
}