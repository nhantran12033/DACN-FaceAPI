import { ListService, TrackByService } from '@abp/ng.core';
import { AfterViewInit, Component, OnInit, ViewChild, inject } from '@angular/core';

import type { ScheduleWithNavigationPropertiesDto } from '../../../proxy/schedules/models';
import { ScheduleViewService } from '../services/schedule.service';
import { ScheduleDetailViewService } from '../services/schedule-detail.service';
import { DatatableComponent } from '@swimlane/ngx-datatable';
@Component({
  template: '',
})
export abstract class AbstractScheduleComponent implements OnInit, AfterViewInit {
  
  @ViewChild('myTable', { static: false }) table: DatatableComponent;
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(ScheduleViewService);
  public readonly serviceDetail = inject(ScheduleDetailViewService);
  protected title = '::Schedules';
  openRows: { [key: string]: boolean } = {};
  ngAfterViewInit(): void {

  }
  ngOnInit() {
    this.service.hookToQuery();
  }
  toggleExpandRow(row) {
    this.openRows[row.id] = !this.openRows[row.id];
    this.table.rowDetail.toggleExpandRow(row);
    this.service.getFormatDetail(row.id);
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
