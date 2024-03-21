using FaceAPI.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using FaceAPI.Shared;

namespace FaceAPI.Salaries
{
    public partial interface ISalariesAppService : IApplicationService
    {
        Task<PagedResultDto<SalaryWithNavigationPropertiesDto>> GetListAsync(GetSalariesInput input);

        Task<SalaryWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<SalaryDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<SalaryDto> CreateAsync(SalaryCreateDto input);

        Task<SalaryDto> UpdateAsync(Guid id, SalaryUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalaryExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}