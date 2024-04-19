import type { GetStaffsInput, StaffCreateDto, StaffDto, StaffExcelDownloadDto, StaffUpdateDto, StaffWithNavigationPropertiesDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto, LookupDto, LookupRequestDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class StaffService {
  apiName = 'Default';
  

  create = (input: StaffCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, StaffDto>({
      method: 'POST',
      url: '/api/app/staffs',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/staffs/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, StaffDto>({
      method: 'GET',
      url: `/api/app/staffs/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getDepartmentLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/app/staffs/department-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>({
      method: 'GET',
      url: '/api/app/staffs/download-token',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetStaffsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<StaffWithNavigationPropertiesDto>>({
      method: 'GET',
      url: '/api/app/staffs',
      params: { filterText: input.filterText, image: input.image, code: input.code, name: input.name, sex: input.sex, birthdayMin: input.birthdayMin, birthdayMax: input.birthdayMax, startWorkMin: input.startWorkMin, startWorkMax: input.startWorkMax, phone: input.phone, email: input.email, address: input.address, debtMin: input.debtMin, debtMax: input.debtMax, note: input.note, departmentId: input.departmentId, titleId: input.titleId, timesheetId: input.timesheetId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListAsExcelFile = (input: StaffExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>({
      method: 'GET',
      responseType: 'blob',
      url: '/api/app/staffs/as-excel-file',
      params: { downloadToken: input.downloadToken, filterText: input.filterText, image: input.image, code: input.code, name: input.name, sex: input.sex, birthdayMin: input.birthdayMin, birthdayMax: input.birthdayMax, startWorkMin: input.startWorkMin, startWorkMax: input.startWorkMax, phone: input.phone, email: input.email, address: input.address, debtMin: input.debtMin, debtMax: input.debtMax, note: input.note, departmentId: input.departmentId, titleId: input.titleId, timesheetId: input.timesheetId },
    },
    { apiName: this.apiName,...config });
  

  getTimesheetLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/app/staffs/timesheet-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getTitleLookup = (input: LookupRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LookupDto<string>>>({
      method: 'GET',
      url: '/api/app/staffs/title-lookup',
      params: { filter: input.filter, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getWithNavigationCodeProperties = (code: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, StaffWithNavigationPropertiesDto>({
      method: 'GET',
      url: `/api/app/staffs/with-navigation-code-properties/${code}`,
    },
    { apiName: this.apiName,...config });
  

  getWithNavigationProperties = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, StaffWithNavigationPropertiesDto>({
      method: 'GET',
      url: `/api/app/staffs/with-navigation-properties/${id}`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: StaffUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, StaffDto>({
      method: 'PUT',
      url: `/api/app/staffs/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
