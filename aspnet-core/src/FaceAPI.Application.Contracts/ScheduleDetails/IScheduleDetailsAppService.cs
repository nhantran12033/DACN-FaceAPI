using FaceAPI.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FaceAPI.ScheduleDetails
{
    public partial interface IScheduleDetailsAppService : IApplicationService
    {
        Task<PagedResultDto<ScheduleDetailWithNavigationPropertiesDto>> GetListAsync(GetScheduleDetailsInput input);

        Task<ScheduleDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<ScheduleDetailDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetScheduleFormatLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ScheduleDetailDto> CreateAsync(ScheduleDetailCreateDto input);

        Task<ScheduleDetailDto> UpdateAsync(Guid id, ScheduleDetailUpdateDto input);
    }
}