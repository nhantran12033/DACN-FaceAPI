using FaceAPI.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using FaceAPI.Shared;
using System.Collections.Generic;

namespace FaceAPI.Schedules
{
    public partial interface ISchedulesAppService : IApplicationService
    {
        Task<PagedResultDto<ScheduleWithNavigationPropertiesDto>> GetListAsync(GetSchedulesInput input);

        Task<ScheduleWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
        Task<List<ScheduleWithNavigationPropertiesDto>> GetWithCodeNavigationPropertiesAsync(Guid id);

        Task<ScheduleDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetStaffLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetScheduleDetailLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ScheduleDto> CreateAsync(ScheduleCreateDto input);

        Task<ScheduleDto> UpdateAsync(Guid id, ScheduleUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ScheduleExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}