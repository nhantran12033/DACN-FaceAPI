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

        public ScheduleDetailsAppServiceBase(IScheduleDetailRepository scheduleDetailRepository, ScheduleDetailManager scheduleDetailManager)
        {

            _scheduleDetailRepository = scheduleDetailRepository;
            _scheduleDetailManager = scheduleDetailManager;
        }

        public virtual async Task<PagedResultDto<ScheduleDetailDto>> GetListAsync(GetScheduleDetailsInput input)
        {
            var totalCount = await _scheduleDetailRepository.GetCountAsync(input.FilterText, input.Name, input.FromMin, input.FromMax, input.ToMin, input.ToMax, input.Note);
            var items = await _scheduleDetailRepository.GetListAsync(input.FilterText, input.Name, input.FromMin, input.FromMax, input.ToMin, input.ToMax, input.Note, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ScheduleDetailDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ScheduleDetail>, List<ScheduleDetailDto>>(items)
            };
        }

        public virtual async Task<ScheduleDetailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ScheduleDetail, ScheduleDetailDto>(await _scheduleDetailRepository.GetAsync(id));
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
            input.From, input.To, input.Name, input.Note
            );

            return ObjectMapper.Map<ScheduleDetail, ScheduleDetailDto>(scheduleDetail);
        }

        [Authorize(FaceAPIPermissions.ScheduleDetails.Edit)]
        public virtual async Task<ScheduleDetailDto> UpdateAsync(Guid id, ScheduleDetailUpdateDto input)
        {

            var scheduleDetail = await _scheduleDetailManager.UpdateAsync(
            id,
            input.From, input.To, input.Name, input.Note, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ScheduleDetail, ScheduleDetailDto>(scheduleDetail);
        }
    }
}