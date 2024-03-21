import type { GetPositionsInput, PositionCreateDto, PositionDto, PositionUpdateDto, PositionWithNavigationPropertiesDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { LookupDto, LookupRequestDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class PositionService {
  apiName = 'Default';
  

  create = (input: PositionCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PositionDto>({
      method: 'POST',
      url: '/api/app/positions',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/positions/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PositionDto>({
      method: 'GET',
      url: `/api/app/positions/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getDepartmentLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/app/positions/department-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetPositionsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<PositionWithNavigationPropertiesDto>>({
      method: 'GET',
      url: '/api/app/positions',
      params: { filterText: input.filterText, code: input.code, name: input.name, note: input.note, departmentId: input.departmentId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getWithNavigationProperties = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PositionWithNavigationPropertiesDto>({
      method: 'GET',
      url: `/api/app/positions/with-navigation-properties/${id}`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: PositionUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PositionDto>({
      method: 'PUT',
      url: `/api/app/positions/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
