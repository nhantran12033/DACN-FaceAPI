import type { GetTitlesInput, TitleCreateDto, TitleDto, TitleUpdateDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TitleService {
  apiName = 'Default';

  create = (input: TitleCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TitleDto>(
      {
        method: 'POST',
        url: '/api/app/titles',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/app/titles/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TitleDto>(
      {
        method: 'GET',
        url: `/api/app/titles/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  getList = (input: GetTitlesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TitleDto>>(
      {
        method: 'GET',
        url: '/api/app/titles',
        params: {
          filterText: input.filterText,
          code: input.code,
          name: input.name,
          note: input.note,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
        },
      },
      { apiName: this.apiName, ...config }
    );

  update = (id: string, input: TitleUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TitleDto>(
      {
        method: 'PUT',
        url: `/api/app/titles/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  constructor(private restService: RestService) {}
}
