import { Injectable, inject } from '@angular/core';
import { ConfirmationService, Confirmation, ToasterService } from '@abp/ng.theme.shared';
import { ABP, ConfigStateService, downloadBlob, ListService, PagedResultDto } from '@abp/ng.core';
import { filter, switchMap, finalize } from 'rxjs/operators';
import { FormsModule } from '@angular/forms'
import type {
  GetTimesheetsInput,
  TimesheetCreateDto,
  TimesheetWithNavigationPropertiesDto,
} from '../../../proxy/timesheets/models';
import { TimesheetService } from '../../../proxy/timesheets/timesheet.service';
import { ScheduleService, ScheduleWithNavigationPropertiesDto } from '../../../proxy/schedules';
import { StaffDto, StaffService, StaffWithNavigationPropertiesDto } from '../../../proxy/staffs';
import { ScheduleDetailDto, ScheduleDetailService, ScheduleDetailWithNavigationPropertiesDto } from '../../../proxy/schedule-details';
import { ScheduleFormatDto, ScheduleFormatService } from '../../../proxy/schedule-formats';

export abstract class AbstractTimesheetViewService {
  protected readonly proxyService = inject(TimesheetService);
  protected readonly proxyScheduleService = inject(ScheduleService);
  protected readonly proxyScheduleDetailService = inject(ScheduleDetailService);
  protected readonly proxyScheduleFormatService = inject(ScheduleFormatService);
  protected readonly proxyStaffService = inject(StaffService);
  protected readonly confirmationService = inject(ConfirmationService);
  protected readonly list = inject(ListService);
  public readonly configState = inject(ConfigStateService);
  public toast = inject(ToasterService);
  isExportToExcelBusy = false; isOpenDetail = false;
  isClear = false;
  dataDto: TimesheetWithNavigationPropertiesDto;
  dataStaffDto: StaffDto;
  dataSchedule: null;
  dataScheduleDetail: null;
  dataScheduleFormat: null;
  dataCreateDto: TimesheetCreateDto;
  dataScheduleFormatDto: ScheduleFormatDto[];
  dataScheduleDto: ScheduleWithNavigationPropertiesDto[];
  dataScheduleDetailDto: ScheduleDetailDto[];
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
  getScheduleData() {
    this.configState.getOne$("currentUser").subscribe(currentUser => {
      this.proxyStaffService.getWithNavigationCodeProperties(currentUser?.userName).subscribe(result => {
        this.proxyScheduleService.getWithCodeNavigationProperties(result.staff?.id).subscribe(resultSchedule => {
          this.dataScheduleDto = resultSchedule;
          this.getScheduleDetailData();
        })
      })
      this.dataScheduleDetail == null;
      this.dataScheduleFormat == null;
    })
    
  }
  getScheduleDetailData() {
    if (this.dataSchedule) {
      this.proxyScheduleService.getWithNavigationProperties(this.dataSchedule).subscribe(result => {
        this.dataScheduleDetailDto = result.scheduleDetails
        this.getScheduleFormatData()
      });
    }
  }
  getScheduleFormatData() {
    if (this.dataScheduleDetail) {
      this.proxyScheduleDetailService.getWithNavigationProperties(this.dataScheduleDetail).subscribe(resultDetail => {
        this.dataScheduleFormatDto = resultDetail.scheduleFormats;
        
      })
    }
    console.log(this.dataScheduleFormat);
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
    this.configState.getOne$("currentUser").subscribe(currentUser => {
      this.dataCreateDto = {
        url: url,
        code: currentUser.userName,
        scheduleId: this.dataSchedule,
        scheduleDetailId: this.dataScheduleDetail,
        scheduleFormatId: this.dataScheduleFormat

      }
      this.proxyService.create(this.dataCreateDto).subscribe(result => {
        if (result) {
          this.toast.success("Face authentication successful", "Success")
        }
      })
    })
    
  }
  getData(id) {
    this.isOpenDetail = true;
    this.proxyService.getWithNavigationProperties(id).subscribe(result => {
      this.dataDto = result;
      this.proxyStaffService.getWithNavigationCodeProperties(this.dataDto.timesheet.code).subscribe(resultStaff => {
        this.dataStaffDto = resultStaff.staff
      })
    })

    
    
  }
}
