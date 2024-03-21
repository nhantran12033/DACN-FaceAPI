using FaceAPI.Shared;
using FaceAPI.Departments;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using FaceAPI.Permissions;
using FaceAPI.Positions;

namespace FaceAPI.Positions
{
    [RemoteService(IsEnabled = false)]
    [Authorize(FaceAPIPermissions.Positions.Default)]
    public abstract class PositionsAppServiceBase : ApplicationService
    {

        protected IPositionRepository _positionRepository;
        protected PositionManager _positionManager;
        protected IRepository<Department, Guid> _departmentRepository;

        public PositionsAppServiceBase(IPositionRepository positionRepository, PositionManager positionManager, IRepository<Department, Guid> departmentRepository)
        {

            _positionRepository = positionRepository;
            _positionManager = positionManager; _departmentRepository = departmentRepository;
        }

        public virtual async Task<PagedResultDto<PositionWithNavigationPropertiesDto>> GetListAsync(GetPositionsInput input)
        {
            var totalCount = await _positionRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.Note, input.DepartmentId);
            var items = await _positionRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.Note, input.DepartmentId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<PositionWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PositionWithNavigationProperties>, List<PositionWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<PositionWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<PositionWithNavigationProperties, PositionWithNavigationPropertiesDto>
                (await _positionRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<PositionDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Position, PositionDto>(await _positionRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input)
        {
            var query = (await _departmentRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Department>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Department>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(FaceAPIPermissions.Positions.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _positionRepository.DeleteAsync(id);
        }

        [Authorize(FaceAPIPermissions.Positions.Create)]
        public virtual async Task<PositionDto> CreateAsync(PositionCreateDto input)
        {

            var position = await _positionManager.CreateAsync(
            input.DepartmentId, input.Code, input.Name, input.Note
            );

            return ObjectMapper.Map<Position, PositionDto>(position);
        }

        [Authorize(FaceAPIPermissions.Positions.Edit)]
        public virtual async Task<PositionDto> UpdateAsync(Guid id, PositionUpdateDto input)
        {

            var position = await _positionManager.UpdateAsync(
            id,
            input.DepartmentId, input.Code, input.Name, input.Note, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Position, PositionDto>(position);
        }
    }
}