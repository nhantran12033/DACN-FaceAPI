import type { GetScheduleDetailsInput, ScheduleDetailCreateDto, ScheduleDetailDto, ScheduleDetailUpdateDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ScheduleDetailService {
  apiName = 'Default';
  

  create = (input: ScheduleDetailCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ScheduleDetailDto>({
      method: 'POST',
      url: '/api/app/schedule-details',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/schedule-details/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ScheduleDetailDto>({
      method: 'GET',
      url: `/api/app/schedule-details/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetScheduleDetailsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ScheduleDetailDto>>({
      method: 'GET',
      url: '/api/app/schedule-details',
      params: { filterText: input.filterText, name: input.name, fromMin: input.fromMin, fromMax: input.fromMax, toMin: input.toMin, toMax: input.toMax, note: input.note, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: ScheduleDetailUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ScheduleDetailDto>({
      method: 'PUT',
      url: `/api/app/schedule-details/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
