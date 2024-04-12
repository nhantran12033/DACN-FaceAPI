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
using FaceAPI.ScheduleFormats;

namespace FaceAPI.ScheduleFormats
{
    [RemoteService(IsEnabled = false)]
    [Authorize(FaceAPIPermissions.ScheduleFormats.Default)]
    public abstract class ScheduleFormatsAppServiceBase : ApplicationService
    {

        protected IScheduleFormatRepository _scheduleFormatRepository;
        protected ScheduleFormatManager _scheduleFormatManager;

        public ScheduleFormatsAppServiceBase(IScheduleFormatRepository scheduleFormatRepository, ScheduleFormatManager scheduleFormatManager)
        {

            _scheduleFormatRepository = scheduleFormatRepository;
            _scheduleFormatManager = scheduleFormatManager;
        }

        public virtual async Task<PagedResultDto<ScheduleFormatDto>> GetListAsync(GetScheduleFormatsInput input)
        {
            var totalCount = await _scheduleFormatRepository.GetCountAsync(input.FilterText, input.Name, input.DateMin, input.DateMax, input.FromHoursMin, input.FromHoursMax, input.ToHoursMin, input.ToHoursMax, input.Note);
            var items = await _scheduleFormatRepository.GetListAsync(input.FilterText, input.Name, input.DateMin, input.DateMax, input.FromHoursMin, input.FromHoursMax, input.ToHoursMin, input.ToHoursMax, input.Note, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ScheduleFormatDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ScheduleFormat>, List<ScheduleFormatDto>>(items)
            };
        }

        public virtual async Task<ScheduleFormatDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ScheduleFormat, ScheduleFormatDto>(await _scheduleFormatRepository.GetAsync(id));
        }

        [Authorize(FaceAPIPermissions.ScheduleFormats.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _scheduleFormatRepository.DeleteAsync(id);
        }

        [Authorize(FaceAPIPermissions.ScheduleFormats.Create)]
        public virtual async Task<ScheduleFormatDto> CreateAsync(ScheduleFormatCreateDto input)
        {

            var scheduleFormat = await _scheduleFormatManager.CreateAsync(
            input.Date, input.FromHours, input.ToHours, input.Name, input.Note
            );

            return ObjectMapper.Map<ScheduleFormat, ScheduleFormatDto>(scheduleFormat);
        }

        [Authorize(FaceAPIPermissions.ScheduleFormats.Edit)]
        public virtual async Task<ScheduleFormatDto> UpdateAsync(Guid id, ScheduleFormatUpdateDto input)
        {

            var scheduleFormat = await _scheduleFormatManager.UpdateAsync(
            id,
            input.Date, input.FromHours, input.ToHours, input.Name, input.Note, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ScheduleFormat, ScheduleFormatDto>(scheduleFormat);
        }
    }
}