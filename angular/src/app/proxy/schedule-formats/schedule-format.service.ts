import type {
  GetScheduleFormatsInput,
  ScheduleFormatCreateDto,
  ScheduleFormatDto,
  ScheduleFormatUpdateDto,
} from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ScheduleFormatService {
  apiName = 'Default';

  create = (input: ScheduleFormatCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ScheduleFormatDto>(
      {
        method: 'POST',
        url: '/api/app/schedule-formats',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/app/schedule-formats/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ScheduleFormatDto>(
      {
        method: 'GET',
        url: `/api/app/schedule-formats/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  getList = (input: GetScheduleFormatsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ScheduleFormatDto>>(
      {
        method: 'GET',
        url: '/api/app/schedule-formats',
        params: {
          filterText: input.filterText,
          name: input.name,
          dateMin: input.dateMin,
          dateMax: input.dateMax,
          fromHoursMin: input.fromHoursMin,
          fromHoursMax: input.fromHoursMax,
          toHoursMin: input.toHoursMin,
          toHoursMax: input.toHoursMax,
          note: input.note,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
        },
      },
      { apiName: this.apiName, ...config }
    );

  update = (id: string, input: ScheduleFormatUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ScheduleFormatDto>(
      {
        method: 'PUT',
        url: `/api/app/schedule-formats/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  constructor(private restService: RestService) {}
}
