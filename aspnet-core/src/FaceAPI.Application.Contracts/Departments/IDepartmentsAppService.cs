using FaceAPI.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FaceAPI.Departments
{
    public partial interface IDepartmentsAppService : IApplicationService
    {
        Task<PagedResultDto<DepartmentWithNavigationPropertiesDto>> GetListAsync(GetDepartmentsInput input);

        Task<DepartmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<DepartmentDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetTitleLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<DepartmentDto> CreateAsync(DepartmentCreateDto input);

        Task<DepartmentDto> UpdateAsync(Guid id, DepartmentUpdateDto input);
    }
}