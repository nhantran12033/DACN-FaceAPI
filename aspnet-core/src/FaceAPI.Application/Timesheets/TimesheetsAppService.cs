using FaceAPI.Shared;
using FaceAPI.ScheduleFormats;
using FaceAPI.ScheduleDetails;
using FaceAPI.Schedules;
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
using FaceAPI.Timesheets;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using FaceAPI.Shared;

namespace FaceAPI.Timesheets
{
    [RemoteService(IsEnabled = false)]
    [Authorize(FaceAPIPermissions.Timesheets.Default)]
    public abstract class TimesheetsAppServiceBase : ApplicationService
    {
        protected IDistributedCache<TimesheetExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        protected ITimesheetRepository _timesheetRepository;
        protected TimesheetManager _timesheetManager;
        protected IRepository<Schedule, Guid> _scheduleRepository;
        protected IRepository<ScheduleDetail, Guid> _scheduleDetailRepository;
        protected IRepository<ScheduleFormat, Guid> _scheduleFormatRepository;

        public TimesheetsAppServiceBase(ITimesheetRepository timesheetRepository, TimesheetManager timesheetManager, IDistributedCache<TimesheetExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Schedule, Guid> scheduleRepository, IRepository<ScheduleDetail, Guid> scheduleDetailRepository, IRepository<ScheduleFormat, Guid> scheduleFormatRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _timesheetRepository = timesheetRepository;
            _timesheetManager = timesheetManager; _scheduleRepository = scheduleRepository;
            _scheduleDetailRepository = scheduleDetailRepository;
            _scheduleFormatRepository = scheduleFormatRepository;
        }

        public virtual async Task<PagedResultDto<TimesheetWithNavigationPropertiesDto>> GetListAsync(GetTimesheetsInput input)
        {
            var totalCount = await _timesheetRepository.GetCountAsync(input.FilterText, input.Code, input.TimeInMin, input.TimeInMax, input.TimeOutMin, input.TimeOutMax, input.HoursWorkMin, input.HoursWorkMax, input.Note, input.ScheduleId, input.ScheduleDetailId, input.ScheduleFormatId);
            var items = await _timesheetRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.TimeInMin, input.TimeInMax, input.TimeOutMin, input.TimeOutMax, input.HoursWorkMin, input.HoursWorkMax, input.Note, input.ScheduleId, input.ScheduleDetailId, input.ScheduleFormatId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TimesheetWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TimesheetWithNavigationProperties>, List<TimesheetWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<TimesheetWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<TimesheetWithNavigationProperties, TimesheetWithNavigationPropertiesDto>
                (await _timesheetRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<TimesheetDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Timesheet, TimesheetDto>(await _timesheetRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetScheduleLookupAsync(LookupRequestDto input)
        {
            var query = (await _scheduleRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Schedule>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Schedule>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetScheduleDetailLookupAsync(LookupRequestDto input)
        {
            var query = (await _scheduleDetailRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ScheduleDetail>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ScheduleDetail>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetScheduleFormatLookupAsync(LookupRequestDto input)
        {
            var query = (await _scheduleFormatRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ScheduleFormat>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ScheduleFormat>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(FaceAPIPermissions.Timesheets.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _timesheetRepository.DeleteAsync(id);
        }

        [Authorize(FaceAPIPermissions.Timesheets.Create)]
        public virtual async Task<TimesheetDto> CreateAsync(TimesheetCreateDto input)
        {

            var timesheet = await _timesheetManager.CreateAsync(
            input.ScheduleId, input.ScheduleDetailId, input.ScheduleFormatId, input.TimeIn, input.TimeOut, input.HoursWork, input.Code, input.Note
            );

            return ObjectMapper.Map<Timesheet, TimesheetDto>(timesheet);
        }

        [Authorize(FaceAPIPermissions.Timesheets.Edit)]
        public virtual async Task<TimesheetDto> UpdateAsync(Guid id, TimesheetUpdateDto input)
        {

            var timesheet = await _timesheetManager.UpdateAsync(
            id,
            input.ScheduleId, input.ScheduleDetailId, input.ScheduleFormatId, input.TimeIn, input.TimeOut, input.HoursWork, input.Code, input.Note, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Timesheet, TimesheetDto>(timesheet);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(TimesheetExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var timesheets = await _timesheetRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.TimeInMin, input.TimeInMax, input.TimeOutMin, input.TimeOutMax, input.HoursWorkMin, input.HoursWorkMax, input.Note);
            var items = timesheets.Select(item => new
            {
                Code = item.Timesheet.Code,
                TimeIn = item.Timesheet.TimeIn,
                TimeOut = item.Timesheet.TimeOut,
                HoursWork = item.Timesheet.HoursWork,
                Note = item.Timesheet.Note,

                Schedule = item.Schedule?.Code,
                ScheduleDetail = item.ScheduleDetail?.Name,
                ScheduleFormat = item.ScheduleFormat?.Name,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Timesheets.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new TimesheetExcelDownloadTokenCacheItem { Token = token },
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