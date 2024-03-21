using FaceAPI.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FaceAPI.Positions
{
    public partial interface IPositionsAppService : IApplicationService
    {
        Task<PagedResultDto<PositionWithNavigationPropertiesDto>> GetListAsync(GetPositionsInput input);

        Task<PositionWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<PositionDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<PositionDto> CreateAsync(PositionCreateDto input);

        Task<PositionDto> UpdateAsync(Guid id, PositionUpdateDto input);
    }
}