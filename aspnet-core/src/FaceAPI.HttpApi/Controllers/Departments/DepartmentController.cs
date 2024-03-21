using FaceAPI.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using FaceAPI.Departments;

namespace FaceAPI.Controllers.Departments
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Department")]
    [Route("api/app/departments")]

    public abstract class DepartmentControllerBase : AbpController
    {
        protected IDepartmentsAppService _departmentsAppService;

        public DepartmentControllerBase(IDepartmentsAppService departmentsAppService)
        {
            _departmentsAppService = departmentsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<DepartmentWithNavigationPropertiesDto>> GetListAsync(GetDepartmentsInput input)
        {
            return _departmentsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<DepartmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _departmentsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<DepartmentDto> GetAsync(Guid id)
        {
            return _departmentsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("title-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetTitleLookupAsync(LookupRequestDto input)
        {
            return _departmentsAppService.GetTitleLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<DepartmentDto> CreateAsync(DepartmentCreateDto input)
        {
            return _departmentsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<DepartmentDto> UpdateAsync(Guid id, DepartmentUpdateDto input)
        {
            return _departmentsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _departmentsAppService.DeleteAsync(id);
        }
    }
}