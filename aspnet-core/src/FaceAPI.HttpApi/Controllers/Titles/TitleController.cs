using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using FaceAPI.Titles;

namespace FaceAPI.Controllers.Titles
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Title")]
    [Route("api/app/titles")]

    public abstract class TitleControllerBase : AbpController
    {
        protected ITitlesAppService _titlesAppService;

        public TitleControllerBase(ITitlesAppService titlesAppService)
        {
            _titlesAppService = titlesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<TitleDto>> GetListAsync(GetTitlesInput input)
        {
            return _titlesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<TitleDto> GetAsync(Guid id)
        {
            return _titlesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<TitleDto> CreateAsync(TitleCreateDto input)
        {
            return _titlesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<TitleDto> UpdateAsync(Guid id, TitleUpdateDto input)
        {
            return _titlesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _titlesAppService.DeleteAsync(id);
        }
    }
}