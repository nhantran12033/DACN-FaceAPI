using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using FaceAPI.ScheduleFormats;

namespace FaceAPI.Controllers.ScheduleFormats
{
    [RemoteService]
    [Area("app")]
    [ControllerName("ScheduleFormat")]
    [Route("api/app/schedule-formats")]

    public abstract class ScheduleFormatControllerBase : AbpController
    {
        protected IScheduleFormatsAppService _scheduleFormatsAppService;

        public ScheduleFormatControllerBase(IScheduleFormatsAppService scheduleFormatsAppService)
        {
            _scheduleFormatsAppService = scheduleFormatsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<ScheduleFormatDto>> GetListAsync(GetScheduleFormatsInput input)
        {
            return _scheduleFormatsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ScheduleFormatDto> GetAsync(Guid id)
        {
            return _scheduleFormatsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<ScheduleFormatDto> CreateAsync(ScheduleFormatCreateDto input)
        {
            return _scheduleFormatsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ScheduleFormatDto> UpdateAsync(Guid id, ScheduleFormatUpdateDto input)
        {
            return _scheduleFormatsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _scheduleFormatsAppService.DeleteAsync(id);
        }
    }
}