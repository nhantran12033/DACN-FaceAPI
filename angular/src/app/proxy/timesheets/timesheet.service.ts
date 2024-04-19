import type { GetTimesheetsInput, TimesheetCreateDto, TimesheetDto, TimesheetExcelDownloadDto, TimesheetUpdateDto, TimesheetWithNavigationPropertiesDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto, LookupDto, LookupRequestDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class TimesheetService {
  apiName = 'Default';
  

  create = (input: TimesheetCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TimesheetDto>({
      method: 'POST',
      url: '/api/app/timesheets',
      body: input,
    },
      { apiName: this.apiName, ...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/timesheets/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TimesheetDto>({
      method: 'GET',
      url: `/api/app/timesheets/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>({
      method: 'GET',
      url: '/api/app/timesheets/download-token',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetTimesheetsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TimesheetWithNavigationPropertiesDto>>({
      method: 'GET',
      url: '/api/app/timesheets',
      params: { filterText: input.filterText, image: input.image, code: input.code, timeMin: input.timeMin, timeMax: input.timeMax, note: input.note, scheduleId: input.scheduleId, scheduleDetailId: input.scheduleDetailId, scheduleFormatId: input.scheduleFormatId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListAsExcelFile = (input: TimesheetExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>({
      method: 'GET',
      responseType: 'blob',
      url: '/api/app/timesheets/as-excel-file',
      params: { downloadToken: input.downloadToken, filterText: input.filterText, image: input.image, code: input.code, timeMin: input.timeMin, timeMax: input.timeMax, note: input.note, scheduleId: input.scheduleId, scheduleDetailId: input.scheduleDetailId, scheduleFormatId: input.scheduleFormatId },
    },
    { apiName: this.apiName,...config });
  

  getScheduleDetailLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/app/timesheets/schedule-detail-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getScheduleFormatLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/app/timesheets/schedule-format-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getScheduleLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/app/timesheets/schedule-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getWithNavigationActiveProperties = (scheduleDetailId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TimesheetWithNavigationPropertiesDto[]>({
      method: 'GET',
      url: `/api/app/timesheets/with-navigation-active-properties/${scheduleDetailId}`,
    },
    { apiName: this.apiName,...config });
  

  getWithNavigationProperties = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TimesheetWithNavigationPropertiesDto>({
      method: 'GET',
      url: `/api/app/timesheets/with-navigation-properties/${id}`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: TimesheetUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TimesheetDto>({
      method: 'PUT',
      url: `/api/app/timesheets/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
