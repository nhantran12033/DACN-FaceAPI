using FaceAPI.Shared;
using FaceAPI.ScheduleDetails;
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
using FaceAPI.Schedules;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using FaceAPI.Shared;

namespace FaceAPI.Schedules
{
    [RemoteService(IsEnabled = false)]
    [Authorize(FaceAPIPermissions.Schedules.Default)]
    public abstract class SchedulesAppServiceBase : ApplicationService
    {
        protected IDistributedCache<ScheduleExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        protected IScheduleRepository _scheduleRepository;
        protected ScheduleManager _scheduleManager;
        protected IRepository<Department, Guid> _departmentRepository;
        protected IRepository<ScheduleDetail, Guid> _scheduleDetailRepository;

        public SchedulesAppServiceBase(IScheduleRepository scheduleRepository, ScheduleManager scheduleManager, IDistributedCache<ScheduleExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Department, Guid> departmentRepository, IRepository<ScheduleDetail, Guid> scheduleDetailRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _scheduleRepository = scheduleRepository;
            _scheduleManager = scheduleManager; _departmentRepository = departmentRepository;
            _scheduleDetailRepository = scheduleDetailRepository;
        }

        public virtual async Task<PagedResultDto<ScheduleWithNavigationPropertiesDto>> GetListAsync(GetSchedulesInput input)
        {
            var totalCount = await _scheduleRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.DateFromMin, input.DateFromMax, input.DateToMin, input.DateToMax, input.Note, input.DepartmentId, input.ScheduleDetailId);
            var items = await _scheduleRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.DateFromMin, input.DateFromMax, input.DateToMin, input.DateToMax, input.Note, input.DepartmentId, input.ScheduleDetailId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ScheduleWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ScheduleWithNavigationProperties>, List<ScheduleWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ScheduleWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ScheduleWithNavigationProperties, ScheduleWithNavigationPropertiesDto>
                (await _scheduleRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<ScheduleDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Schedule, ScheduleDto>(await _scheduleRepository.GetAsync(id));
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

        [Authorize(FaceAPIPermissions.Schedules.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _scheduleRepository.DeleteAsync(id);
        }

        [Authorize(FaceAPIPermissions.Schedules.Create)]
        public virtual async Task<ScheduleDto> CreateAsync(ScheduleCreateDto input)
        {

            var schedule = await _scheduleManager.CreateAsync(
            input.ScheduleDetailIds, input.DepartmentId, input.DateFrom, input.DateTo, input.Code, input.Name, input.Note
            );

            return ObjectMapper.Map<Schedule, ScheduleDto>(schedule);
        }

        [Authorize(FaceAPIPermissions.Schedules.Edit)]
        public virtual async Task<ScheduleDto> UpdateAsync(Guid id, ScheduleUpdateDto input)
        {

            var schedule = await _scheduleManager.UpdateAsync(
            id,
            input.ScheduleDetailIds, input.DepartmentId, input.DateFrom, input.DateTo, input.Code, input.Name, input.Note, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Schedule, ScheduleDto>(schedule);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ScheduleExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var schedules = await _scheduleRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.DateFromMin, input.DateFromMax, input.DateToMin, input.DateToMax, input.Note);
            var items = schedules.Select(item => new
            {
                Code = item.Schedule.Code,
                Name = item.Schedule.Name,
                DateFrom = item.Schedule.DateFrom,
                DateTo = item.Schedule.DateTo,
                Note = item.Schedule.Note,

                Department = item.Department?.Name,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Schedules.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ScheduleExcelDownloadTokenCacheItem { Token = token },
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