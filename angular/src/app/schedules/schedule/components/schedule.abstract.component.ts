import { ListService, TrackByService } from '@abp/ng.core';
import { AfterViewInit, Component, OnInit, ViewChild, inject } from '@angular/core';

import type { ScheduleWithNavigationPropertiesDto } from '../../../proxy/schedules/models';
import { ScheduleViewService } from '../services/schedule.service';
import { ScheduleDetailViewService } from '../services/schedule-detail.service';
import { DatatableComponent } from '@swimlane/ngx-datatable';
import { ToasterService } from '@abp/ng.theme.shared';
import { WebcamImage } from 'ngx-webcam';
import { TimesheetCreateDto } from '../../../proxy/timesheets/models';
@Component({
  template: '',
})
export abstract class AbstractScheduleComponent implements OnInit, AfterViewInit {
  protected readonly toastr = inject(ToasterService)
  @ViewChild('myTable', { static: false }) table: DatatableComponent;
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(ScheduleViewService);
  public readonly serviceDetail = inject(ScheduleDetailViewService);
  protected title = '::Schedules';
  public dataCreate: TimesheetCreateDto;
  openRows: { [key: string]: boolean } = {};
  ngAfterViewInit(): void {

  }
  ngOnInit() {
    this.service.hookToQuery();
  }
  private currentlyOpenedRowId: any = null;
  private currentlyOpenedName: any = null;
  toggleExpandRow(row) {
    if (this.currentlyOpenedRowId === row.id) {
      // Nếu hàng này đã mở, đóng nó lại
      this.openRows[row.id] = !this.openRows[row.id]
      this.table.rowDetail.toggleExpandRow(row);
      this.currentlyOpenedRowId = null;
    } else {
      if (this.currentlyOpenedRowId !== null) {
        this.toastr.error('Close row name is ' + this.currentlyOpenedName + ' before opening new row');

      }
      else {
        this.openRows[row.id] = !this.openRows[row.id]
        this.table.rowDetail.toggleExpandRow(row);
        this.currentlyOpenedRowId = row.id;
        this.currentlyOpenedName = row.name;
        this.service.getFormatDetail(row.id);
      }
      
          
      
    }
  }
  getData(id) {
    this.serviceDetail.getData(id);
  }
  clearFilters() {
    this.service.clearFilters();
  }

  showForm() {
    this.serviceDetail.showForm();
  }

  create() {
    this.serviceDetail.selected = undefined;
    this.serviceDetail.showForm();
  }

  update(record: ScheduleWithNavigationPropertiesDto) {
    this.serviceDetail.update(record);
  }

  delete(record: ScheduleWithNavigationPropertiesDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }
}
