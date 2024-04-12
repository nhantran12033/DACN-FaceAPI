using FaceAPI.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using FaceAPI.Shared;

namespace FaceAPI.Staffs
{
    public partial interface IStaffsAppService : IApplicationService
    {
        Task<PagedResultDto<StaffWithNavigationPropertiesDto>> GetListAsync(GetStaffsInput input);

        Task<StaffWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
        Task<StaffWithNavigationPropertiesDto> GetWithNavigationCodePropertiesAsync(string code);

        Task<StaffDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetTitleLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetTimesheetLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<StaffDto> CreateAsync(StaffCreateDto input);

        Task<StaffDto> UpdateAsync(Guid id, StaffUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(StaffExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}