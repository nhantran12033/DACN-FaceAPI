import { Injectable, inject } from '@angular/core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ABP, ListService, PagedResultDto } from '@abp/ng.core';
import { filter, switchMap } from 'rxjs/operators';
import type {
  GetScheduleDetailsInput,
  ScheduleDetailWithNavigationPropertiesDto,
} from '../../../proxy/schedule-details/models';
import { ScheduleDetailService } from '../../../proxy/schedule-details/schedule-detail.service';

export abstract class AbstractScheduleDetailViewService {
  protected readonly proxyService = inject(ScheduleDetailService);
  protected readonly confirmationService = inject(ConfirmationService);
  protected readonly list = inject(ListService);

  public readonly getWithNavigationProperties = this.proxyService.getWithNavigationProperties;

  data: PagedResultDto<ScheduleDetailWithNavigationPropertiesDto> = {
    items: [],
    totalCount: 0,
  };

  filters = {} as GetScheduleDetailsInput;

  delete(record: ScheduleDetailWithNavigationPropertiesDto) {
    this.confirmationService
      .warn('::DeleteConfirmationMessage', '::AreYouSure', { messageLocalizationParams: [] })
      .pipe(
        filter(status => status === Confirmation.Status.confirm),
        switchMap(() => this.proxyService.delete(record.scheduleDetail.id))
      )
      .subscribe(this.list.get);
  }

  hookToQuery() {
    const getData = (query: ABP.PageQueryParams) =>
      this.proxyService.getList({
        ...query,
        ...this.filters,
        filterText: query.filter,
      });

    const setData = (list: PagedResultDto<ScheduleDetailWithNavigationPropertiesDto>) =>
      (this.data = list);

    this.list.hookToQuery(getData).subscribe(setData);
  }

  clearFilters() {
    this.filters = {} as GetScheduleDetailsInput;
    this.list.get();
  }
}
