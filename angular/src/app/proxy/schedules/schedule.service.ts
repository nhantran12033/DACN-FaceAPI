import type { GetSchedulesInput, ScheduleCreateDto, ScheduleDto, ScheduleExcelDownloadDto, ScheduleUpdateDto, ScheduleWithNavigationPropertiesDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto, LookupDto, LookupRequestDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class ScheduleService {
  apiName = 'Default';
  

  create = (input: ScheduleCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ScheduleDto>({
      method: 'POST',
      url: '/api/app/schedules',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/schedules/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ScheduleDto>({
      method: 'GET',
      url: `/api/app/schedules/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>({
      method: 'GET',
      url: '/api/app/schedules/download-token',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetSchedulesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ScheduleWithNavigationPropertiesDto>>({
      method: 'GET',
      url: '/api/app/schedules',
      params: { filterText: input.filterText, code: input.code, name: input.name, dateFromMin: input.dateFromMin, dateFromMax: input.dateFromMax, dateToMin: input.dateToMin, dateToMax: input.dateToMax, note: input.note, staffId: input.staffId, scheduleDetailId: input.scheduleDetailId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListAsExcelFile = (input: ScheduleExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>({
      method: 'GET',
      responseType: 'blob',
      url: '/api/app/schedules/as-excel-file',
      params: { downloadToken: input.downloadToken, filterText: input.filterText, code: input.code, name: input.name, dateFromMin: input.dateFromMin, dateFromMax: input.dateFromMax, dateToMin: input.dateToMin, dateToMax: input.dateToMax, note: input.note, staffId: input.staffId, scheduleDetailId: input.scheduleDetailId },
    },
    { apiName: this.apiName,...config });
  

  getScheduleDetailLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/app/schedules/schedule-detail-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getStaffLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/app/schedules/staff-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getWithCodeNavigationProperties = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ScheduleWithNavigationPropertiesDto[]>({
      method: 'GET',
      url: `/api/app/schedules/with-code-navigation-properties/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getWithNavigationProperties = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ScheduleWithNavigationPropertiesDto>({
      method: 'GET',
      url: `/api/app/schedules/with-navigation-properties/${id}`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: ScheduleUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ScheduleDto>({
      method: 'PUT',
      url: `/api/app/schedules/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
