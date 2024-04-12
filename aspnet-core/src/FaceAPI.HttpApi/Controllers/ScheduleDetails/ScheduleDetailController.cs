using FaceAPI.Shared;
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
        public virtual Task<PagedResultDto<ScheduleDetailWithNavigationPropertiesDto>> GetListAsync(GetScheduleDetailsInput input)
        {
            return _scheduleDetailsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<ScheduleDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _scheduleDetailsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ScheduleDetailDto> GetAsync(Guid id)
        {
            return _scheduleDetailsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("schedule-format-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetScheduleFormatLookupAsync(LookupRequestDto input)
        {
            return _scheduleDetailsAppService.GetScheduleFormatLookupAsync(input);
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