import type { DepartmentCreateDto, DepartmentDto, DepartmentUpdateDto, DepartmentWithNavigationPropertiesDto, GetDepartmentsInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { LookupDto, LookupRequestDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  apiName = 'Default';
  

  create = (input: DepartmentCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DepartmentDto>({
      method: 'POST',
      url: '/api/app/departments',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/departments/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DepartmentDto>({
      method: 'GET',
      url: `/api/app/departments/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetDepartmentsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<DepartmentWithNavigationPropertiesDto>>({
      method: 'GET',
      url: '/api/app/departments',
      params: { filterText: input.filterText, code: input.code, name: input.name, dateMin: input.dateMin, dateMax: input.dateMax, note: input.note, titleId: input.titleId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getTitleLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/app/departments/title-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getWithNavigationProperties = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DepartmentWithNavigationPropertiesDto>({
      method: 'GET',
      url: `/api/app/departments/with-navigation-properties/${id}`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: DepartmentUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DepartmentDto>({
      method: 'PUT',
      url: `/api/app/departments/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
