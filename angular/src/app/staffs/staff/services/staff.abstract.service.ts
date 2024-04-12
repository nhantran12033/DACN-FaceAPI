import { Injectable, inject} from '@angular/core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ABP, downloadBlob, ListService, PagedResultDto, ConfigStateService } from '@abp/ng.core';
import { filter, switchMap, finalize } from 'rxjs/operators';
import type {
  GetStaffsInput,
  StaffWithNavigationPropertiesDto,
} from '../../../proxy/staffs/models';
import { StaffService } from '../../../proxy/staffs/staff.service';

export abstract class AbstractStaffViewService {
  protected readonly proxyService = inject(StaffService);
  protected readonly confirmationService = inject(ConfirmationService);
  protected readonly list = inject(ListService);
  public readonly configState = inject(ConfigStateService);
  public readonly getWithNavigationProperties = this.proxyService.getWithNavigationProperties;

  isExportToExcelBusy = false;

  data: PagedResultDto<StaffWithNavigationPropertiesDto> = {
    items: [],
    totalCount: 0,
  };

  filters = {} as GetStaffsInput;
  getUserName() {
    this.configState.getOne$("currentUser").subscribe(currentUser => {
      console.log(currentUser)
    })
  }
  delete(record: StaffWithNavigationPropertiesDto) {
    this.confirmationService
      .warn('::DeleteConfirmationMessage', '::AreYouSure', { messageLocalizationParams: [] })
      .pipe(
        filter(status => status === Confirmation.Status.confirm),
        switchMap(() => this.proxyService.delete(record.staff.id))
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

    const setData = (list: PagedResultDto<StaffWithNavigationPropertiesDto>) => (this.data = list);

    this.list.hookToQuery(getData).subscribe(setData);
  }

  clearFilters() {
    this.filters = {} as GetStaffsInput;
    this.list.get();
  }

  exportToExcel() {
    this.isExportToExcelBusy = true;
    this.proxyService
      .getDownloadToken()
      .pipe(
        switchMap(({ token }) =>
          this.proxyService.getListAsExcelFile({
            downloadToken: token,
            filterText: this.list.filter,
          })
        ),
        finalize(() => (this.isExportToExcelBusy = false))
      )
      .subscribe(result => {
        downloadBlob(result, 'Staff.xlsx');
      });
  }
}
