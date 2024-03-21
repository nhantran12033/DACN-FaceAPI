using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FaceAPI.Titles
{
    public partial interface ITitlesAppService : IApplicationService
    {
        Task<PagedResultDto<TitleDto>> GetListAsync(GetTitlesInput input);

        Task<TitleDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<TitleDto> CreateAsync(TitleCreateDto input);

        Task<TitleDto> UpdateAsync(Guid id, TitleUpdateDto input);
    }
}