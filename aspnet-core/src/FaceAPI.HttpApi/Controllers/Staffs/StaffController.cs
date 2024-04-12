using FaceAPI.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using FaceAPI.Staffs;
using Volo.Abp.Content;
using FaceAPI.Shared;


namespace FaceAPI.Controllers.Staffs
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Staff")]
    [Route("api/app/staffs")]

    public abstract class StaffControllerBase : AbpController
    {
        protected IStaffsAppService _staffsAppService;

        public StaffControllerBase(IStaffsAppService staffsAppService)
        {
            _staffsAppService = staffsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<StaffWithNavigationPropertiesDto>> GetListAsync(GetStaffsInput input)
        {
            return _staffsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<StaffWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _staffsAppService.GetWithNavigationPropertiesAsync(id);
        }
        [HttpGet]
        [Route("with-navigation-code-properties/{code}")]
        public virtual Task<StaffWithNavigationPropertiesDto> GetWithNavigationCodePropertiesAsync(string code)
        {
            return _staffsAppService.GetWithNavigationCodePropertiesAsync(code);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<StaffDto> GetAsync(Guid id)
        {
            return _staffsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("department-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input)
        {
            return _staffsAppService.GetDepartmentLookupAsync(input);
        }

        [HttpGet]
        [Route("title-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetTitleLookupAsync(LookupRequestDto input)
        {
            return _staffsAppService.GetTitleLookupAsync(input);
        }

        [HttpGet]
        [Route("timesheet-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetTimesheetLookupAsync(LookupRequestDto input)
        {
            return _staffsAppService.GetTimesheetLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<StaffDto> CreateAsync(StaffCreateDto input)
        {
            return _staffsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<StaffDto> UpdateAsync(Guid id, StaffUpdateDto input)
        {
            return _staffsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _staffsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(StaffExcelDownloadDto input)
        {
            return _staffsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _staffsAppService.GetDownloadTokenAsync();
        }
    }
}