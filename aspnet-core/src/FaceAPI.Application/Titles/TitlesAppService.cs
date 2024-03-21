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
using FaceAPI.Titles;

namespace FaceAPI.Titles
{
    [RemoteService(IsEnabled = false)]
    [Authorize(FaceAPIPermissions.Titles.Default)]
    public abstract class TitlesAppServiceBase : ApplicationService
    {

        protected ITitleRepository _titleRepository;
        protected TitleManager _titleManager;

        public TitlesAppServiceBase(ITitleRepository titleRepository, TitleManager titleManager)
        {

            _titleRepository = titleRepository;
            _titleManager = titleManager;
        }

        public virtual async Task<PagedResultDto<TitleDto>> GetListAsync(GetTitlesInput input)
        {
            var totalCount = await _titleRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.Note);
            var items = await _titleRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.Note, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TitleDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Title>, List<TitleDto>>(items)
            };
        }

        public virtual async Task<TitleDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Title, TitleDto>(await _titleRepository.GetAsync(id));
        }

        [Authorize(FaceAPIPermissions.Titles.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _titleRepository.DeleteAsync(id);
        }

        [Authorize(FaceAPIPermissions.Titles.Create)]
        public virtual async Task<TitleDto> CreateAsync(TitleCreateDto input)
        {

            var title = await _titleManager.CreateAsync(
            input.Code, input.Name, input.Note
            );

            return ObjectMapper.Map<Title, TitleDto>(title);
        }

        [Authorize(FaceAPIPermissions.Titles.Edit)]
        public virtual async Task<TitleDto> UpdateAsync(Guid id, TitleUpdateDto input)
        {

            var title = await _titleManager.UpdateAsync(
            id,
            input.Code, input.Name, input.Note, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Title, TitleDto>(title);
        }
    }
}