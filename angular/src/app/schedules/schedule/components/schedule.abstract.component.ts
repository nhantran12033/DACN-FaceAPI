import { ListService, TrackByService } from '@abp/ng.core';
import { Component, OnInit, inject } from '@angular/core';

import type { ScheduleWithNavigationPropertiesDto } from '../../../proxy/schedules/models';
import { ScheduleViewService } from '../services/schedule.service';
import { ScheduleDetailViewService } from '../services/schedule-detail.service';

@Component({
  template: '',
})
export abstract class AbstractScheduleComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(ScheduleViewService);
  public readonly serviceDetail = inject(ScheduleDetailViewService);
  protected title = '::Schedules';

  ngOnInit() {
    this.service.hookToQuery();
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
