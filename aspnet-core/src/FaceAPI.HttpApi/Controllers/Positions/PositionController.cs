using FaceAPI.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using FaceAPI.Positions;

namespace FaceAPI.Controllers.Positions
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Position")]
    [Route("api/app/positions")]

    public abstract class PositionControllerBase : AbpController
    {
        protected IPositionsAppService _positionsAppService;

        public PositionControllerBase(IPositionsAppService positionsAppService)
        {
            _positionsAppService = positionsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<PositionWithNavigationPropertiesDto>> GetListAsync(GetPositionsInput input)
        {
            return _positionsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<PositionWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _positionsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<PositionDto> GetAsync(Guid id)
        {
            return _positionsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("department-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input)
        {
            return _positionsAppService.GetDepartmentLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<PositionDto> CreateAsync(PositionCreateDto input)
        {
            return _positionsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<PositionDto> UpdateAsync(Guid id, PositionUpdateDto input)
        {
            return _positionsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _positionsAppService.DeleteAsync(id);
        }
    }
}