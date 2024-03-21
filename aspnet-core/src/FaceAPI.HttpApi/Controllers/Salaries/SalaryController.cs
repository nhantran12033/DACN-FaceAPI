using FaceAPI.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using FaceAPI.Salaries;
using Volo.Abp.Content;
using FaceAPI.Shared;

namespace FaceAPI.Controllers.Salaries
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Salary")]
    [Route("api/app/salaries")]

    public abstract class SalaryControllerBase : AbpController
    {
        protected ISalariesAppService _salariesAppService;

        public SalaryControllerBase(ISalariesAppService salariesAppService)
        {
            _salariesAppService = salariesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<SalaryWithNavigationPropertiesDto>> GetListAsync(GetSalariesInput input)
        {
            return _salariesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<SalaryWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _salariesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<SalaryDto> GetAsync(Guid id)
        {
            return _salariesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("department-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input)
        {
            return _salariesAppService.GetDepartmentLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<SalaryDto> CreateAsync(SalaryCreateDto input)
        {
            return _salariesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<SalaryDto> UpdateAsync(Guid id, SalaryUpdateDto input)
        {
            return _salariesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _salariesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalaryExcelDownloadDto input)
        {
            return _salariesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _salariesAppService.GetDownloadTokenAsync();
        }
    }
}