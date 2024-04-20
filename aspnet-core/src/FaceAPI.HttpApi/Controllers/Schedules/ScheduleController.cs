using FaceAPI.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using FaceAPI.Schedules;
using Volo.Abp.Content;
using FaceAPI.Shared;
using System.Collections.Generic;

namespace FaceAPI.Controllers.Schedules
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Schedule")]
    [Route("api/app/schedules")]

    public abstract class ScheduleControllerBase : AbpController
    {
        protected ISchedulesAppService _schedulesAppService;

        public ScheduleControllerBase(ISchedulesAppService schedulesAppService)
        {
            _schedulesAppService = schedulesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<ScheduleWithNavigationPropertiesDto>> GetListAsync(GetSchedulesInput input)
        {
            return _schedulesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<ScheduleWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _schedulesAppService.GetWithNavigationPropertiesAsync(id);
        }
        [HttpGet]
        [Route("with-code-navigation-properties/{id}")]
        public virtual Task<List<ScheduleWithNavigationPropertiesDto>> GetWithCodeNavigationPropertiesAsync(Guid id)
        {
            return _schedulesAppService.GetWithCodeNavigationPropertiesAsync(id);
        }
        [HttpGet]
        [Route("{id}")]
        public virtual Task<ScheduleDto> GetAsync(Guid id)
        {
            return _schedulesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("staff-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetStaffLookupAsync(LookupRequestDto input)
        {
            return _schedulesAppService.GetStaffLookupAsync(input);
        }

        [HttpGet]
        [Route("schedule-detail-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetScheduleDetailLookupAsync(LookupRequestDto input)
        {
            return _schedulesAppService.GetScheduleDetailLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<ScheduleDto> CreateAsync(ScheduleCreateDto input)
        {
            return _schedulesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ScheduleDto> UpdateAsync(Guid id, ScheduleUpdateDto input)
        {
            return _schedulesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _schedulesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(ScheduleExcelDownloadDto input)
        {
            return _schedulesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _schedulesAppService.GetDownloadTokenAsync();
        }
    }
}