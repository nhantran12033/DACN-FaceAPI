using FaceAPI.Shared;
using FaceAPI.Timesheets;
using FaceAPI.Titles;
using FaceAPI.Departments;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using FaceAPI.Permissions;
using FaceAPI.Staffs;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using FaceAPI.Shared;


namespace FaceAPI.Staffs
{
    [RemoteService(IsEnabled = false)]
    [Authorize(FaceAPIPermissions.Staffs.Default)]
    public abstract class StaffsAppServiceBase : ApplicationService
    {
        protected IDistributedCache<StaffExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        protected IStaffRepository _staffRepository;
        protected StaffManager _staffManager;
        protected IRepository<Department, Guid> _departmentRepository;
        protected IRepository<Title, Guid> _titleRepository;
        protected IRepository<Timesheet, Guid> _timesheetRepository;

        public StaffsAppServiceBase(IStaffRepository staffRepository, StaffManager staffManager, IDistributedCache<StaffExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Department, Guid> departmentRepository, IRepository<Title, Guid> titleRepository, IRepository<Timesheet, Guid> timesheetRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _staffRepository = staffRepository;
            _staffManager = staffManager; _departmentRepository = departmentRepository;
            _titleRepository = titleRepository;
            _timesheetRepository = timesheetRepository;
        }
        public virtual async Task<PagedResultDto<StaffWithNavigationPropertiesDto>> GetListAsync(GetStaffsInput input)
        {
            var totalCount = await _staffRepository.GetCountAsync(input.FilterText, input.Image, input.Code, input.Name, input.Sex, input.BirthdayMin, input.BirthdayMax, input.StartWorkMin, input.StartWorkMax, input.Phone, input.Email, input.Address, input.DebtMin, input.DebtMax, input.Note, input.DepartmentId, input.TitleId, input.TimesheetId);
            var items = await _staffRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Image, input.Code, input.Name, input.Sex, input.BirthdayMin, input.BirthdayMax, input.StartWorkMin, input.StartWorkMax, input.Phone, input.Email, input.Address, input.DebtMin, input.DebtMax, input.Note, input.DepartmentId, input.TitleId, input.TimesheetId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<StaffWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<StaffWithNavigationProperties>, List<StaffWithNavigationPropertiesDto>>(items)
            };
        }
        public virtual async Task<StaffWithNavigationPropertiesDto> GetWithNavigationCodePropertiesAsync(string code)
        {
            return ObjectMapper.Map<StaffWithNavigationProperties, StaffWithNavigationPropertiesDto>
                (await _staffRepository.GetWithNavigationCodePropertiesAsync(code));
        }
        public virtual async Task<StaffWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<StaffWithNavigationProperties, StaffWithNavigationPropertiesDto>
                (await _staffRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<StaffDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Staff, StaffDto>(await _staffRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input)
        {
            var query = (await _departmentRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Department>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Department>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetTitleLookupAsync(LookupRequestDto input)
        {
            var query = (await _titleRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Title>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Title>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetTimesheetLookupAsync(LookupRequestDto input)
        {
            var query = (await _timesheetRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Timesheet>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Timesheet>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(FaceAPIPermissions.Staffs.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _staffRepository.DeleteAsync(id);
        }

        [Authorize(FaceAPIPermissions.Staffs.Create)]
        public virtual async Task<StaffDto> CreateAsync(StaffCreateDto input)
        {

            var staff = await _staffManager.CreateAsync(
            input.TimesheetIds, input.DepartmentId, input.TitleId, input.Birthday, input.StartWork, input.Debt, input.Image, input.Code, input.Name, input.Sex, input.Phone, input.Email, input.Address, input.Note
            );

            return ObjectMapper.Map<Staff, StaffDto>(staff);
        }

        [Authorize(FaceAPIPermissions.Staffs.Edit)]
        public virtual async Task<StaffDto> UpdateAsync(Guid id, StaffUpdateDto input)
        {

            var staff = await _staffManager.UpdateAsync(
            id,
            input.TimesheetIds, input.DepartmentId, input.TitleId, input.Birthday, input.StartWork, input.Debt, input.Image, input.Code, input.Name, input.Sex, input.Phone, input.Email, input.Address, input.Note, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Staff, StaffDto>(staff);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(StaffExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var staffs = await _staffRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Image, input.Code, input.Name, input.Sex, input.BirthdayMin, input.BirthdayMax, input.StartWorkMin, input.StartWorkMax, input.Phone, input.Email, input.Address, input.DebtMin, input.DebtMax, input.Note);
            var items = staffs.Select(item => new
            {
                Image = item.Staff.Image,
                Code = item.Staff.Code,
                Name = item.Staff.Name,
                Sex = item.Staff.Sex,
                Birthday = item.Staff.Birthday,
                StartWork = item.Staff.StartWork,
                Phone = item.Staff.Phone,
                Email = item.Staff.Email,
                Address = item.Staff.Address,
                Debt = item.Staff.Debt,
                Note = item.Staff.Note,

                Department = item.Department?.Name,
                Title = item.Title?.Name,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Staffs.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new StaffExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}