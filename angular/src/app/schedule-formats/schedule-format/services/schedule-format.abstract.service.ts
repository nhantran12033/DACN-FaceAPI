import { Injectable, inject } from '@angular/core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ABP, ListService, PagedResultDto } from '@abp/ng.core';
import { filter, switchMap } from 'rxjs/operators';
import type {
  GetScheduleFormatsInput,
  ScheduleFormatDto,
} from '../../../proxy/schedule-formats/models';
import { ScheduleFormatService } from '../../../proxy/schedule-formats/schedule-format.service';

export abstract class AbstractScheduleFormatViewService {
  protected readonly proxyService = inject(ScheduleFormatService);
  protected readonly confirmationService = inject(ConfirmationService);
  protected readonly list = inject(ListService);

  data: PagedResultDto<ScheduleFormatDto> = {
    items: [],
    totalCount: 0,
  };

  filters = {} as GetScheduleFormatsInput;

  delete(record: ScheduleFormatDto) {
    this.confirmationService
      .warn('::DeleteConfirmationMessage', '::AreYouSure', { messageLocalizationParams: [] })
      .pipe(
        filter(status => status === Confirmation.Status.confirm),
        switchMap(() => this.proxyService.delete(record.id))
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

    const setData = (list: PagedResultDto<ScheduleFormatDto>) => (this.data = list);

    this.list.hookToQuery(getData).subscribe(setData);
  }

  clearFilters() {
    this.filters = {} as GetScheduleFormatsInput;
    this.list.get();
  }
}
