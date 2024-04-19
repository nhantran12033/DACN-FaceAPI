import { Injectable, inject } from '@angular/core';
import { ConfirmationService, Confirmation, ToasterService } from '@abp/ng.theme.shared';
import { ABP, downloadBlob, ListService, PagedResultDto } from '@abp/ng.core';
import { filter, switchMap, finalize } from 'rxjs/operators';
import type {
  GetTimesheetsInput,
  TimesheetCreateDto,
  TimesheetWithNavigationPropertiesDto,
} from '../../../proxy/timesheets/models';
import { TimesheetService } from '../../../proxy/timesheets/timesheet.service';

export abstract class AbstractTimesheetViewService {
  protected readonly proxyService = inject(TimesheetService);
  protected readonly confirmationService = inject(ConfirmationService);
  protected readonly list = inject(ListService);
  public toast = inject(ToasterService);
  isExportToExcelBusy = false;
  dataCreateDto: TimesheetCreateDto;
  data: PagedResultDto<TimesheetWithNavigationPropertiesDto> = {
    items: [],
    totalCount: 0,
  };

  filters = {} as GetTimesheetsInput;

  delete(record: TimesheetWithNavigationPropertiesDto) {
    this.confirmationService
      .warn('::DeleteConfirmationMessage', '::AreYouSure', { messageLocalizationParams: [] })
      .pipe(
        filter(status => status === Confirmation.Status.confirm),
        switchMap(() => this.proxyService.delete(record.timesheet.id))
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

    const setData = (list: PagedResultDto<TimesheetWithNavigationPropertiesDto>) =>
      (this.data = list);

    this.list.hookToQuery(getData).subscribe(setData);
  }

  clearFilters() {
    this.filters = {} as GetTimesheetsInput;
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
        downloadBlob(result, 'Timesheet.xlsx');
      });
  }
  createDto(url) {
    this.dataCreateDto = {
      url: url,
      scheduleId: 'e935bb6f-13c0-b288-9f39-3a11bfe012ae',
      scheduleDetailId: '246810c6-9250-08a0-f83c-3a11ed626f4e',
      scheduleFormatId: 'f19ceaf0-7a47-bf80-967d-3a11ed4751d8'
      
    }
    this.proxyService.create(this.dataCreateDto).subscribe(result => {
      if (result) {
        this.toast.success("Face authentication successful", "Success")
      }
    })
  }
}
