import { ListService, TrackByService } from '@abp/ng.core';
import { Component, OnInit, inject } from '@angular/core';

import type { ScheduleDetailWithNavigationPropertiesDto } from '../../../proxy/schedule-details/models';
import { ScheduleDetailViewService } from '../services/schedule-detail.service';
import { ScheduleDetailDetailViewService } from '../services/schedule-detail-detail.service';

@Component({
  template: '',
})
export abstract class AbstractScheduleDetailComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(ScheduleDetailViewService);
  public readonly serviceDetail = inject(ScheduleDetailDetailViewService);
  protected title = '::ScheduleDetails';

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

  update(record: ScheduleDetailWithNavigationPropertiesDto) {
    this.serviceDetail.update(record);
  }

  delete(record: ScheduleDetailWithNavigationPropertiesDto) {
    this.service.delete(record);
  }
}
