using FaceAPI.Shared;
using FaceAPI.ScheduleFormats;
using FaceAPI.ScheduleDetails;
using FaceAPI.Schedules;
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
using FaceAPI.Timesheets;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using FaceAPI.Shared;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace FaceAPI.Timesheets
{
    [RemoteService(IsEnabled = false)]
    [Authorize(FaceAPIPermissions.Timesheets.Default)]
    public abstract class TimesheetsAppServiceBase : ApplicationService
    {
        protected IDistributedCache<TimesheetExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        protected ITimesheetRepository _timesheetRepository;
        protected TimesheetManager _timesheetManager;
        protected IRepository<Schedule, Guid> _scheduleRepository;
        protected IRepository<ScheduleDetail, Guid> _scheduleDetailRepository;
        protected IRepository<ScheduleFormat, Guid> _scheduleFormatRepository;
        private readonly HttpClient _httpClient;
        public TimesheetsAppServiceBase(IHttpClientFactory httpClientFactory, ITimesheetRepository timesheetRepository, TimesheetManager timesheetManager, IDistributedCache<TimesheetExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Schedule, Guid> scheduleRepository, IRepository<ScheduleDetail, Guid> scheduleDetailRepository, IRepository<ScheduleFormat, Guid> scheduleFormatRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _timesheetRepository = timesheetRepository;
            _timesheetManager = timesheetManager; _scheduleRepository = scheduleRepository;
            _scheduleDetailRepository = scheduleDetailRepository;
            _scheduleFormatRepository = scheduleFormatRepository;
            _httpClient = httpClientFactory.CreateClient("Face");
        }

        public virtual async Task<PagedResultDto<TimesheetWithNavigationPropertiesDto>> GetListAsync(GetTimesheetsInput input)
        {
            var totalCount = await _timesheetRepository.GetCountAsync(input.FilterText, input.Image, input.Code, input.TimeMin, input.TimeMax, input.Note, input.ScheduleId, input.ScheduleDetailId, input.ScheduleFormatId);
            var items = await _timesheetRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Image, input.Code, input.TimeMin, input.TimeMax, input.Note, input.ScheduleId, input.ScheduleDetailId, input.ScheduleFormatId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TimesheetWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TimesheetWithNavigationProperties>, List<TimesheetWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<TimesheetWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<TimesheetWithNavigationProperties, TimesheetWithNavigationPropertiesDto>
                (await _timesheetRepository.GetWithNavigationPropertiesAsync(id));
        }
        public virtual async Task<List<TimesheetWithNavigationPropertiesDto>> GetWithNavigationActivePropertiesAsync(Guid scheduleDetailId)
        {
            return ObjectMapper.Map<List<TimesheetWithNavigationProperties>, List<TimesheetWithNavigationPropertiesDto>>
                (await _timesheetRepository.GetWithNavigationActivePropertiesAsync(scheduleDetailId));
        }
        public virtual async Task<TimesheetDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Timesheet, TimesheetDto>(await _timesheetRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetScheduleLookupAsync(LookupRequestDto input)
        {
            var query = (await _scheduleRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Schedule>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Schedule>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetScheduleDetailLookupAsync(LookupRequestDto input)
        {
            var query = (await _scheduleDetailRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ScheduleDetail>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ScheduleDetail>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetScheduleFormatLookupAsync(LookupRequestDto input)
        {
            var query = (await _scheduleFormatRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ScheduleFormat>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ScheduleFormat>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(FaceAPIPermissions.Timesheets.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _timesheetRepository.DeleteAsync(id);
        }
        

        [Authorize(FaceAPIPermissions.Timesheets.Create)]
        public virtual async Task<TimesheetDto> CreateAsync(TimesheetCreateDto input)
        {
            var data = new
            {
                cloud_name = "dpnoxwgmn",
                apikey = "741269675425769",
                apisecret = "yKI3FqtESmX653wbFPisFltxeqI"
            };
            Account account = new Account(data.cloud_name, data.apikey, data.apisecret);
            Cloudinary cloudinary = new Cloudinary(account);
            var ImguploadParams = new ImageUploadParams()
            {
                File = new FileDescription(input.Url)
            };
            var ImguploadResult = cloudinary.Upload(ImguploadParams);
            
            var postData = new
            {
                providers = "amazon",
                file_url = ImguploadResult.SecureUri
            };
            var json = JsonConvert.SerializeObject(postData);

            // Create a StringContent object that can be sent as the request body, specifying media type as JSON
            using var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("https://api.edenai.run/v2/image/face_recognition/recognize", content);
            if (response.IsSuccessStatusCode)
            {
                var responseDto = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<ApiResponse>(responseDto);
                if (dto.Amazon.Items.Any())
                {
                    var timesheet = await _timesheetManager.CreateAsync(
                input.ScheduleId, input.ScheduleDetailId, input.ScheduleFormatId, DateTime.Now, ImguploadResult.SecureUri.ToString(), input.Code, input.Note);

                    return ObjectMapper.Map<Timesheet, TimesheetDto>(timesheet);
                }
                else
                {
                    throw new UserFriendlyException("Invalid Face");
                    
                }

            }
            else
            {
                throw new UserFriendlyException("Cannot call API");
            }
        }

        [Authorize(FaceAPIPermissions.Timesheets.Edit)]
        public virtual async Task<TimesheetDto> UpdateAsync(Guid id, TimesheetUpdateDto input)
        {

            var timesheet = await _timesheetManager.UpdateAsync(
            id,
            input.ScheduleId, input.ScheduleDetailId, input.ScheduleFormatId, input.Time, input.Image, input.Code, input.Note, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Timesheet, TimesheetDto>(timesheet);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(TimesheetExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var timesheets = await _timesheetRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Image, input.Code, input.TimeMin, input.TimeMax, input.Note);
            var items = timesheets.Select(item => new
            {
                Image = item.Timesheet.Image,
                Code = item.Timesheet.Code,
                Time = item.Timesheet.Time,
                Note = item.Timesheet.Note,

                Schedule = item.Schedule?.Code,
                ScheduleDetail = item.ScheduleDetail?.Name,
                ScheduleFormat = item.ScheduleFormat?.Name,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Timesheets.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new TimesheetExcelDownloadTokenCacheItem { Token = token },
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