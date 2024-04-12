using FaceAPI.Titles;
using FaceAPI.Departments;
using FaceAPI.Timesheets;
using FaceAPI.Timesheets;
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

namespace FaceAPI.Staffs
{
    public abstract class EfCoreStaffRepositoryBase : EfCoreRepository<FaceAPIDbContext, Staff, Guid>
    {
        public EfCoreStaffRepositoryBase(IDbContextProvider<FaceAPIDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<StaffWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id).Include(x => x.Timesheets)
                .Select(staff => new StaffWithNavigationProperties
                {
                    Staff = staff,
                    Department = dbContext.Set<Department>().FirstOrDefault(c => c.Id == staff.DepartmentId),
                    Title = dbContext.Set<Title>().FirstOrDefault(c => c.Id == staff.TitleId),
                    Timesheets = (from staffTimesheets in staff.Timesheets
                                  join _timesheet in dbContext.Set<Timesheet>() on staffTimesheets.TimesheetId equals _timesheet.Id
                                  select _timesheet).ToList()
                }).FirstOrDefault();
        }
        public virtual async Task<StaffWithNavigationProperties> GetWithNavigationCodePropertiesAsync(string code, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Code == code).Include(x => x.Timesheets)
                .Select(staff => new StaffWithNavigationProperties
                {
                    Staff = staff,
                    Department = dbContext.Set<Department>().FirstOrDefault(c => c.Id == staff.DepartmentId),
                    Title = dbContext.Set<Title>().FirstOrDefault(c => c.Id == staff.TitleId),
                    Timesheets = (from staffTimesheets in staff.Timesheets
                                  join _timesheet in dbContext.Set<Timesheet>() on staffTimesheets.TimesheetId equals _timesheet.Id
                                  select _timesheet).ToList()
                }).FirstOrDefault();
        }
        public virtual async Task<List<StaffWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? image = null,
            string? code = null,
            string? name = null,
            string? sex = null,
            DateTime? birthdayMin = null,
            DateTime? birthdayMax = null,
            DateTime? startWorkMin = null,
            DateTime? startWorkMax = null,
            string? phone = null,
            string? email = null,
            string? address = null,
            int? debtMin = null,
            int? debtMax = null,
            string? note = null,
            Guid? departmentId = null,
            Guid? titleId = null,
            Guid? timesheetId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, image, code, name, sex, birthdayMin, birthdayMax, startWorkMin, startWorkMax, phone, email, address, debtMin, debtMax, note, departmentId, titleId, timesheetId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? StaffConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<StaffWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from staff in (await GetDbSetAsync())
                   join department in (await GetDbContextAsync()).Set<Department>() on staff.DepartmentId equals department.Id into departments
                   from department in departments.DefaultIfEmpty()
                   join title in (await GetDbContextAsync()).Set<Title>() on staff.TitleId equals title.Id into titles
                   from title in titles.DefaultIfEmpty()
                   select new StaffWithNavigationProperties
                   {
                       Staff = staff,
                       Department = department,
                       Title = title,
                       Timesheets = new List<Timesheet>()
                   };
        }

        protected virtual IQueryable<StaffWithNavigationProperties> ApplyFilter(
            IQueryable<StaffWithNavigationProperties> query,
            string? filterText,
            string? image = null,
            string? code = null,
            string? name = null,
            string? sex = null,
            DateTime? birthdayMin = null,
            DateTime? birthdayMax = null,
            DateTime? startWorkMin = null,
            DateTime? startWorkMax = null,
            string? phone = null,
            string? email = null,
            string? address = null,
            int? debtMin = null,
            int? debtMax = null,
            string? note = null,
            Guid? departmentId = null,
            Guid? titleId = null,
            Guid? timesheetId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Staff.Image!.Contains(filterText!) || e.Staff.Code!.Contains(filterText!) || e.Staff.Name!.Contains(filterText!) || e.Staff.Sex!.Contains(filterText!) || e.Staff.Phone!.Contains(filterText!) || e.Staff.Email!.Contains(filterText!) || e.Staff.Address!.Contains(filterText!) || e.Staff.Note!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(image), e => e.Staff.Image.Contains(image))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Staff.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Staff.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(sex), e => e.Staff.Sex.Contains(sex))
                    .WhereIf(birthdayMin.HasValue, e => e.Staff.Birthday >= birthdayMin!.Value)
                    .WhereIf(birthdayMax.HasValue, e => e.Staff.Birthday <= birthdayMax!.Value)
                    .WhereIf(startWorkMin.HasValue, e => e.Staff.StartWork >= startWorkMin!.Value)
                    .WhereIf(startWorkMax.HasValue, e => e.Staff.StartWork <= startWorkMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(phone), e => e.Staff.Phone.Contains(phone))
                    .WhereIf(!string.IsNullOrWhiteSpace(email), e => e.Staff.Email.Contains(email))
                    .WhereIf(!string.IsNullOrWhiteSpace(address), e => e.Staff.Address.Contains(address))
                    .WhereIf(debtMin.HasValue, e => e.Staff.Debt >= debtMin!.Value)
                    .WhereIf(debtMax.HasValue, e => e.Staff.Debt <= debtMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Staff.Note.Contains(note))
                    .WhereIf(departmentId != null && departmentId != Guid.Empty, e => e.Department != null && e.Department.Id == departmentId)
                    .WhereIf(titleId != null && titleId != Guid.Empty, e => e.Title != null && e.Title.Id == titleId)
                    .WhereIf(timesheetId != null && timesheetId != Guid.Empty, e => e.Staff.Timesheets.Any(x => x.TimesheetId == timesheetId));
        }

        public virtual async Task<List<Staff>> GetListAsync(
            string? filterText = null,
            string? image = null,
            string? code = null,
            string? name = null,
            string? sex = null,
            DateTime? birthdayMin = null,
            DateTime? birthdayMax = null,
            DateTime? startWorkMin = null,
            DateTime? startWorkMax = null,
            string? phone = null,
            string? email = null,
            string? address = null,
            int? debtMin = null,
            int? debtMax = null,
            string? note = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, image, code, name, sex, birthdayMin, birthdayMax, startWorkMin, startWorkMax, phone, email, address, debtMin, debtMax, note);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? StaffConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? image = null,
            string? code = null,
            string? name = null,
            string? sex = null,
            DateTime? birthdayMin = null,
            DateTime? birthdayMax = null,
            DateTime? startWorkMin = null,
            DateTime? startWorkMax = null,
            string? phone = null,
            string? email = null,
            string? address = null,
            int? debtMin = null,
            int? debtMax = null,
            string? note = null,
            Guid? departmentId = null,
            Guid? titleId = null,
            Guid? timesheetId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, image, code, name, sex, birthdayMin, birthdayMax, startWorkMin, startWorkMax, phone, email, address, debtMin, debtMax, note, departmentId, titleId, timesheetId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Staff> ApplyFilter(
            IQueryable<Staff> query,
            string? filterText = null,
            string? image = null,
            string? code = null,
            string? name = null,
            string? sex = null,
            DateTime? birthdayMin = null,
            DateTime? birthdayMax = null,
            DateTime? startWorkMin = null,
            DateTime? startWorkMax = null,
            string? phone = null,
            string? email = null,
            string? address = null,
            int? debtMin = null,
            int? debtMax = null,
            string? note = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Image!.Contains(filterText!) || e.Code!.Contains(filterText!) || e.Name!.Contains(filterText!) || e.Sex!.Contains(filterText!) || e.Phone!.Contains(filterText!) || e.Email!.Contains(filterText!) || e.Address!.Contains(filterText!) || e.Note!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(image), e => e.Image.Contains(image))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(sex), e => e.Sex.Contains(sex))
                    .WhereIf(birthdayMin.HasValue, e => e.Birthday >= birthdayMin!.Value)
                    .WhereIf(birthdayMax.HasValue, e => e.Birthday <= birthdayMax!.Value)
                    .WhereIf(startWorkMin.HasValue, e => e.StartWork >= startWorkMin!.Value)
                    .WhereIf(startWorkMax.HasValue, e => e.StartWork <= startWorkMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(phone), e => e.Phone.Contains(phone))
                    .WhereIf(!string.IsNullOrWhiteSpace(email), e => e.Email.Contains(email))
                    .WhereIf(!string.IsNullOrWhiteSpace(address), e => e.Address.Contains(address))
                    .WhereIf(debtMin.HasValue, e => e.Debt >= debtMin!.Value)
                    .WhereIf(debtMax.HasValue, e => e.Debt <= debtMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Note.Contains(note));
        }
    }
}