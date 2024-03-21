using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FaceAPI.ScheduleDetails
{
    public partial interface IScheduleDetailsAppService : IApplicationService
    {
        Task<PagedResultDto<ScheduleDetailDto>> GetListAsync(GetScheduleDetailsInput input);

        Task<ScheduleDetailDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ScheduleDetailDto> CreateAsync(ScheduleDetailCreateDto input);

        Task<ScheduleDetailDto> UpdateAsync(Guid id, ScheduleDetailUpdateDto input);
    }
}