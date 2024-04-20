import { Injectable, ViewChild, inject } from '@angular/core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ABP, ConfigStateService, downloadBlob, ListService, PagedResultDto } from '@abp/ng.core';
import { filter, switchMap, finalize } from 'rxjs/operators';
import type {
  GetSchedulesInput,
  GetSchedulesStaffIdInput,
  ScheduleWithNavigationPropertiesDto,
} from '../../../proxy/schedules/models';
import { ScheduleService } from '../../../proxy/schedules/schedule.service';
import { ScheduleDetailService, ScheduleDetailWithNavigationPropertiesDto } from '../../../proxy/schedule-details';
import { GetTimesheetsInput, TimesheetService } from '../../../proxy/timesheets';
import { ScheduleDetailViewService } from './schedule-detail.service';
import { StaffService, StaffWithNavigationPropertiesDto } from '../../../proxy/staffs';

export abstract class AbstractScheduleViewService {
  protected readonly proxyService = inject(ScheduleService);
  protected readonly proxyDetailService = inject(ScheduleDetailService);
  protected readonly proxyStaffService = inject(StaffService);
  protected readonly proxySheetService = inject(TimesheetService);
  protected readonly confirmationService = inject(ConfirmationService);
  protected readonly list = inject(ListService);
  protected readonly listStaff = inject(ListService);
  public readonly configState = inject(ConfigStateService);
  public readonly serviceDetail = inject(ScheduleDetailViewService);
  public readonly getWithNavigationProperties = this.proxyService.getWithNavigationProperties;
  dataStaffDto: StaffWithNavigationPropertiesDto;
  activeId = [];
  activeDetailId = [];
  isActive: boolean;
  filter: GetTimesheetsInput;
  isExportToExcelBusy = false;
  dataDetailDto: ScheduleDetailWithNavigationPropertiesDto;
  dataTimeSheetDto: ScheduleDetailWithNavigationPropertiesDto;
  data: PagedResultDto<ScheduleWithNavigationPropertiesDto> = {
    items: [],
    totalCount: 0,
  };

  filters = {} as GetSchedulesInput;
  filterStaffIds = {} as GetSchedulesStaffIdInput;
  delete(record: ScheduleWithNavigationPropertiesDto) {
    this.confirmationService
      .warn('::DeleteConfirmationMessage', '::AreYouSure', { messageLocalizationParams: [] })
      .pipe(
        filter(status => status === Confirmation.Status.confirm),
        switchMap(() => this.proxyService.delete(record.schedule.id))
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

    const setData = (list: PagedResultDto<ScheduleWithNavigationPropertiesDto>) =>
      (this.data = list);

    this.list.hookToQuery(getData).subscribe(setData);
  }
  hookToQueryStaffId() {
    this.configState.getOne$("currentUser").subscribe(currentUser => {
      var user = currentUser.userName;
      this.proxyStaffService.getWithNavigationCodeProperties(user).subscribe(result => {
        this.filterStaffIds = {
          maxResultCount: 100,
          staffId: result.staff.id
        } as GetSchedulesStaffIdInput;
        const getData = (query: ABP.PageQueryParams) =>
          this.proxyService.getList({
            ...query,
            ...this.filterStaffIds,
            filterText: query.filter,
          });

        const setData = (listStaff: PagedResultDto<ScheduleWithNavigationPropertiesDto>) =>
          (this.data = listStaff);

        this.listStaff.hookToQuery(getData).subscribe(setData);
      })
    })
    
  }
  clearFilters() {
    this.filters = {} as GetSchedulesInput;
    this.list.get();
    this.configState.getOne$("currentUser").subscribe(currentUser => {
      var user = currentUser.userName;
      this.proxyStaffService.getWithNavigationCodeProperties(user).subscribe(result => {
        this.filterStaffIds = {
          maxResultCount: 100,
          staffId: result.staff.id
        } as GetSchedulesStaffIdInput;
        this.listStaff.get();
      });
    });

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
        downloadBlob(result, 'Schedule.xlsx');
      });
  }

  getFormatDetail(id) {
    this.proxyDetailService.getWithNavigationProperties(id).subscribe(result => {
      this.dataDetailDto = result;
    });
  }

}
