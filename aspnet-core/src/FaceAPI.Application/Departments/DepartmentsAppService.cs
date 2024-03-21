using FaceAPI.Shared;
using FaceAPI.Titles;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using FaceAPI.Permissions;
using FaceAPI.Departments;

namespace FaceAPI.Departments
{
    [RemoteService(IsEnabled = false)]
    [Authorize(FaceAPIPermissions.Departments.Default)]
    public abstract class DepartmentsAppServiceBase : ApplicationService
    {

        protected IDepartmentRepository _departmentRepository;
        protected DepartmentManager _departmentManager;
        protected IRepository<Title, Guid> _titleRepository;

        public DepartmentsAppServiceBase(IDepartmentRepository departmentRepository, DepartmentManager departmentManager, IRepository<Title, Guid> titleRepository)
        {

            _departmentRepository = departmentRepository;
            _departmentManager = departmentManager; _titleRepository = titleRepository;
        }

        public virtual async Task<PagedResultDto<DepartmentWithNavigationPropertiesDto>> GetListAsync(GetDepartmentsInput input)
        {
            var totalCount = await _departmentRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.DateMin, input.DateMax, input.Note, input.TitleId);
            var items = await _departmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.DateMin, input.DateMax, input.Note, input.TitleId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DepartmentWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<DepartmentWithNavigationProperties>, List<DepartmentWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<DepartmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<DepartmentWithNavigationProperties, DepartmentWithNavigationPropertiesDto>
                (await _departmentRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<DepartmentDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Department, DepartmentDto>(await _departmentRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetTitleLookupAsync(LookupRequestDto input)
        {
            var query = (await _titleRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Title>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Title>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(FaceAPIPermissions.Departments.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _departmentRepository.DeleteAsync(id);
        }

        [Authorize(FaceAPIPermissions.Departments.Create)]
        public virtual async Task<DepartmentDto> CreateAsync(DepartmentCreateDto input)
        {

            var department = await _departmentManager.CreateAsync(
            input.TitleIds, input.Date, input.Code, input.Name, input.Note
            );

            return ObjectMapper.Map<Department, DepartmentDto>(department);
        }

        [Authorize(FaceAPIPermissions.Departments.Edit)]
        public virtual async Task<DepartmentDto> UpdateAsync(Guid id, DepartmentUpdateDto input)
        {

            var department = await _departmentManager.UpdateAsync(
            id,
            input.TitleIds, input.Date, input.Code, input.Name, input.Note, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Department, DepartmentDto>(department);
        }
    }
}