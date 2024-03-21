import { Injectable, inject } from '@angular/core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ABP, ListService, PagedResultDto } from '@abp/ng.core';
import { filter, switchMap } from 'rxjs/operators';
import type {
  GetDepartmentsInput,
  DepartmentWithNavigationPropertiesDto,
} from '../../../proxy/departments/models';
import { DepartmentService } from '../../../proxy/departments/department.service';

export abstract class AbstractDepartmentViewService {
  protected readonly proxyService = inject(DepartmentService);
  protected readonly confirmationService = inject(ConfirmationService);
  protected readonly list = inject(ListService);

  public readonly getWithNavigationProperties = this.proxyService.getWithNavigationProperties;

  data: PagedResultDto<DepartmentWithNavigationPropertiesDto> = {
    items: [],
    totalCount: 0,
  };

  filters = {} as GetDepartmentsInput;

  delete(record: DepartmentWithNavigationPropertiesDto) {
    this.confirmationService
      .warn('::DeleteConfirmationMessage', '::AreYouSure', { messageLocalizationParams: [] })
      .pipe(
        filter(status => status === Confirmation.Status.confirm),
        switchMap(() => this.proxyService.delete(record.department.id))
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

    const setData = (list: PagedResultDto<DepartmentWithNavigationPropertiesDto>) =>
      (this.data = list);

    this.list.hookToQuery(getData).subscribe(setData);
  }

  clearFilters() {
    this.filters = {} as GetDepartmentsInput;
    this.list.get();
  }
}
