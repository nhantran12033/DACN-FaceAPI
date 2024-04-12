using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FaceAPI.ScheduleFormats
{
    public partial interface IScheduleFormatsAppService : IApplicationService
    {
        Task<PagedResultDto<ScheduleFormatDto>> GetListAsync(GetScheduleFormatsInput input);

        Task<ScheduleFormatDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ScheduleFormatDto> CreateAsync(ScheduleFormatCreateDto input);

        Task<ScheduleFormatDto> UpdateAsync(Guid id, ScheduleFormatUpdateDto input);
    }
}