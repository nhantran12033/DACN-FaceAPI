import type { GetSalariesInput, SalaryCreateDto, SalaryDto, SalaryExcelDownloadDto, SalaryUpdateDto, SalaryWithNavigationPropertiesDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto, LookupDto, LookupRequestDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class SalaryService {
  apiName = 'Default';
  

  create = (input: SalaryCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SalaryDto>({
      method: 'POST',
      url: '/api/app/salaries',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/salaries/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SalaryDto>({
      method: 'GET',
      url: `/api/app/salaries/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getDepartmentLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/app/salaries/department-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>({
      method: 'GET',
      url: '/api/app/salaries/download-token',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetSalariesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<SalaryWithNavigationPropertiesDto>>({
      method: 'GET',
      url: '/api/app/salaries',
      params: { filterText: input.filterText, code: input.code, allowanceMin: input.allowanceMin, allowanceMax: input.allowanceMax, basicMin: input.basicMin, basicMax: input.basicMax, bonusMin: input.bonusMin, bonusMax: input.bonusMax, totalMin: input.totalMin, totalMax: input.totalMax, departmentId: input.departmentId, titleId: input.titleId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListAsExcelFile = (input: SalaryExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>({
      method: 'GET',
      responseType: 'blob',
      url: '/api/app/salaries/as-excel-file',
      params: { downloadToken: input.downloadToken, filterText: input.filterText, code: input.code, allowanceMin: input.allowanceMin, allowanceMax: input.allowanceMax, basicMin: input.basicMin, basicMax: input.basicMax, bonusMin: input.bonusMin, bonusMax: input.bonusMax, totalMin: input.totalMin, totalMax: input.totalMax, departmentId: input.departmentId, titleId: input.titleId },
    },
    { apiName: this.apiName,...config });
  

  getTitleLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/app/salaries/title-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getWithNavigationProperties = (departmentId: string, titleId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SalaryWithNavigationPropertiesDto>({
      method: 'GET',
      url: `/api/app/salaries/with-navigation-properties/${departmentId}/${titleId}`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: SalaryUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SalaryDto>({
      method: 'PUT',
      url: `/api/app/salaries/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
