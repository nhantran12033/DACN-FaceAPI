using FaceAPI.Shared;
using FaceAPI.Titles;
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
using FaceAPI.Salaries;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using FaceAPI.Shared;

namespace FaceAPI.Salaries
{
    [RemoteService(IsEnabled = false)]
    [Authorize(FaceAPIPermissions.Salaries.Default)]
    public abstract class SalariesAppServiceBase : ApplicationService
    {
        protected IDistributedCache<SalaryExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        protected ISalaryRepository _salaryRepository;
        protected SalaryManager _salaryManager;
        protected IRepository<Department, Guid> _departmentRepository;
        protected IRepository<Title, Guid> _titleRepository;

        public SalariesAppServiceBase(ISalaryRepository salaryRepository, SalaryManager salaryManager, IDistributedCache<SalaryExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Department, Guid> departmentRepository, IRepository<Title, Guid> titleRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _salaryRepository = salaryRepository;
            _salaryManager = salaryManager; _departmentRepository = departmentRepository;
            _titleRepository = titleRepository;
        }

        public virtual async Task<PagedResultDto<SalaryWithNavigationPropertiesDto>> GetListAsync(GetSalariesInput input)
        {
            var totalCount = await _salaryRepository.GetCountAsync(input.FilterText, input.Code, input.AllowanceMin, input.AllowanceMax, input.BasicMin, input.BasicMax, input.BonusMin, input.BonusMax, input.TotalMin, input.TotalMax, input.DepartmentId, input.TitleId);
            var items = await _salaryRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.AllowanceMin, input.AllowanceMax, input.BasicMin, input.BasicMax, input.BonusMin, input.BonusMax, input.TotalMin, input.TotalMax, input.DepartmentId, input.TitleId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<SalaryWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SalaryWithNavigationProperties>, List<SalaryWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<SalaryWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid departmentId, Guid titleId)
        {
            return ObjectMapper.Map<SalaryWithNavigationProperties, SalaryWithNavigationPropertiesDto>
                (await _salaryRepository.GetWithNavigationPropertiesAsync(departmentId, titleId));
        }

        public virtual async Task<SalaryDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Salary, SalaryDto>(await _salaryRepository.GetAsync(id));
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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetTitleLookupAsync(LookupRequestDto input)
        {
            var query = (await _titleRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Title>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Title>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(FaceAPIPermissions.Salaries.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _salaryRepository.DeleteAsync(id);
        }

        [Authorize(FaceAPIPermissions.Salaries.Create)]
        public virtual async Task<SalaryDto> CreateAsync(SalaryCreateDto input)
        {

            var salary = await _salaryManager.CreateAsync(
            input.DepartmentId, input.TitleId, input.Allowance, input.Basic, input.Bonus, input.Code
            );

            return ObjectMapper.Map<Salary, SalaryDto>(salary);
        }

        [Authorize(FaceAPIPermissions.Salaries.Edit)]
        public virtual async Task<SalaryDto> UpdateAsync(Guid id, SalaryUpdateDto input)
        {

            var salary = await _salaryManager.UpdateAsync(
            id,
            input.DepartmentId, input.TitleId, input.Allowance, input.Basic, input.Bonus, input.Code, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Salary, SalaryDto>(salary);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalaryExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var salaries = await _salaryRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.AllowanceMin, input.AllowanceMax, input.BasicMin, input.BasicMax, input.BonusMin, input.BonusMax, input.TotalMin, input.TotalMax);
            var items = salaries.Select(item => new
            {
                Code = item.Salary.Code,
                Allowance = item.Salary.Allowance,
                Basic = item.Salary.Basic,
                Bonus = item.Salary.Bonus,
                Total = item.Salary.Total,

                Department = item.Department?.Name,
                Title = item.Title?.Name,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Salaries.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new SalaryExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}