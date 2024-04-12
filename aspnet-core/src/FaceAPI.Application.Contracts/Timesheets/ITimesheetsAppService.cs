using FaceAPI.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using FaceAPI.Shared;

namespace FaceAPI.Timesheets
{
    public partial interface ITimesheetsAppService : IApplicationService
    {
        Task<PagedResultDto<TimesheetWithNavigationPropertiesDto>> GetListAsync(GetTimesheetsInput input);

        Task<TimesheetWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<TimesheetDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetScheduleLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetScheduleDetailLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetScheduleFormatLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<TimesheetDto> CreateAsync(TimesheetCreateDto input);

        Task<TimesheetDto> UpdateAsync(Guid id, TimesheetUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(TimesheetExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}