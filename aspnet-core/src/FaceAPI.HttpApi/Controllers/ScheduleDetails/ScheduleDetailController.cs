using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using FaceAPI.ScheduleDetails;

namespace FaceAPI.Controllers.ScheduleDetails
{
    [RemoteService]
    [Area("app")]
    [ControllerName("ScheduleDetail")]
    [Route("api/app/schedule-details")]

    public abstract class ScheduleDetailControllerBase : AbpController
    {
        protected IScheduleDetailsAppService _scheduleDetailsAppService;

        public ScheduleDetailControllerBase(IScheduleDetailsAppService scheduleDetailsAppService)
        {
            _scheduleDetailsAppService = scheduleDetailsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<ScheduleDetailDto>> GetListAsync(GetScheduleDetailsInput input)
        {
            return _scheduleDetailsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ScheduleDetailDto> GetAsync(Guid id)
        {
            return _scheduleDetailsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<ScheduleDetailDto> CreateAsync(ScheduleDetailCreateDto input)
        {
            return _scheduleDetailsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ScheduleDetailDto> UpdateAsync(Guid id, ScheduleDetailUpdateDto input)
        {
            return _scheduleDetailsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _scheduleDetailsAppService.DeleteAsync(id);
        }
    }
}