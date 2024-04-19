using FaceAPI.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using FaceAPI.Timesheets;
using Volo.Abp.Content;
using FaceAPI.Shared;
using System.Collections.Generic;

namespace FaceAPI.Controllers.Timesheets
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Timesheet")]
    [Route("api/app/timesheets")]

    public abstract class TimesheetControllerBase : AbpController
    {
        protected ITimesheetsAppService _timesheetsAppService;

        public TimesheetControllerBase(ITimesheetsAppService timesheetsAppService)
        {
            _timesheetsAppService = timesheetsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<TimesheetWithNavigationPropertiesDto>> GetListAsync(GetTimesheetsInput input)
        {
            return _timesheetsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<TimesheetWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _timesheetsAppService.GetWithNavigationPropertiesAsync(id);
        }
        [HttpGet]
        [Route("with-navigation-active-properties/{scheduleDetailId}")]
        public virtual Task<List<TimesheetWithNavigationPropertiesDto>> GetWithNavigationActivePropertiesAsync(Guid scheduleDetailId)
        {
            return _timesheetsAppService.GetWithNavigationActivePropertiesAsync(scheduleDetailId);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<TimesheetDto> GetAsync(Guid id)
        {
            return _timesheetsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("schedule-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetScheduleLookupAsync(LookupRequestDto input)
        {
            return _timesheetsAppService.GetScheduleLookupAsync(input);
        }

        [HttpGet]
        [Route("schedule-detail-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetScheduleDetailLookupAsync(LookupRequestDto input)
        {
            return _timesheetsAppService.GetScheduleDetailLookupAsync(input);
        }

        [HttpGet]
        [Route("schedule-format-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetScheduleFormatLookupAsync(LookupRequestDto input)
        {
            return _timesheetsAppService.GetScheduleFormatLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<TimesheetDto> CreateAsync(TimesheetCreateDto input)
        {
            return _timesheetsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<TimesheetDto> UpdateAsync(Guid id, TimesheetUpdateDto input)
        {
            return _timesheetsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _timesheetsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(TimesheetExcelDownloadDto input)
        {
            return _timesheetsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _timesheetsAppService.GetDownloadTokenAsync();
        }
    }
}