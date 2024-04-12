using FaceAPI.Shared;
using FaceAPI.ScheduleFormats;
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
using FaceAPI.ScheduleDetails;

namespace FaceAPI.ScheduleDetails
{
    [RemoteService(IsEnabled = false)]
    [Authorize(FaceAPIPermissions.ScheduleDetails.Default)]
    public abstract class ScheduleDetailsAppServiceBase : ApplicationService
    {

        protected IScheduleDetailRepository _scheduleDetailRepository;
        protected ScheduleDetailManager _scheduleDetailManager;
        protected IRepository<ScheduleFormat, Guid> _scheduleFormatRepository;

        public ScheduleDetailsAppServiceBase(IScheduleDetailRepository scheduleDetailRepository, ScheduleDetailManager scheduleDetailManager, IRepository<ScheduleFormat, Guid> scheduleFormatRepository)
        {

            _scheduleDetailRepository = scheduleDetailRepository;
            _scheduleDetailManager = scheduleDetailManager; _scheduleFormatRepository = scheduleFormatRepository;
        }

        public virtual async Task<PagedResultDto<ScheduleDetailWithNavigationPropertiesDto>> GetListAsync(GetScheduleDetailsInput input)
        {
            var totalCount = await _scheduleDetailRepository.GetCountAsync(input.FilterText, input.Name, input.FromDateMin, input.FromDateMax, input.ToDateMin, input.ToDateMax, input.Note, input.ScheduleFormatId);
            var items = await _scheduleDetailRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Name, input.FromDateMin, input.FromDateMax, input.ToDateMin, input.ToDateMax, input.Note, input.ScheduleFormatId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ScheduleDetailWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ScheduleDetailWithNavigationProperties>, List<ScheduleDetailWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<ScheduleDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<ScheduleDetailWithNavigationProperties, ScheduleDetailWithNavigationPropertiesDto>
                (await _scheduleDetailRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<ScheduleDetailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ScheduleDetail, ScheduleDetailDto>(await _scheduleDetailRepository.GetAsync(id));
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

        [Authorize(FaceAPIPermissions.ScheduleDetails.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _scheduleDetailRepository.DeleteAsync(id);
        }

        [Authorize(FaceAPIPermissions.ScheduleDetails.Create)]
        public virtual async Task<ScheduleDetailDto> CreateAsync(ScheduleDetailCreateDto input)
        {

            var scheduleDetail = await _scheduleDetailManager.CreateAsync(
            input.ScheduleFormatIds, input.FromDate, input.ToDate, input.Name, input.Note
            );

            return ObjectMapper.Map<ScheduleDetail, ScheduleDetailDto>(scheduleDetail);
        }

        [Authorize(FaceAPIPermissions.ScheduleDetails.Edit)]
        public virtual async Task<ScheduleDetailDto> UpdateAsync(Guid id, ScheduleDetailUpdateDto input)
        {

            var scheduleDetail = await _scheduleDetailManager.UpdateAsync(
            id,
            input.ScheduleFormatIds, input.FromDate, input.ToDate, input.Name, input.Note, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ScheduleDetail, ScheduleDetailDto>(scheduleDetail);
        }
    }
}