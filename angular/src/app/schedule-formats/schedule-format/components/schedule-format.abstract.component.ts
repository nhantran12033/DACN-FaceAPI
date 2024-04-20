import { ListService, PermissionService, TrackByService } from '@abp/ng.core';
import { Component, OnInit, inject } from '@angular/core';

import type { ScheduleFormatDto } from '../../../proxy/schedule-formats/models';
import { ScheduleFormatViewService } from '../services/schedule-format.service';
import { ScheduleFormatDetailViewService } from '../services/schedule-format-detail.service';

@Component({
  template: '',
})
export abstract class AbstractScheduleFormatComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly listStaff = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(ScheduleFormatViewService);
  public readonly serviceDetail = inject(ScheduleFormatDetailViewService);
  public permission = inject(PermissionService);
  protected title = '::ScheduleFormats';
  canPermission: boolean;
  ngOnInit() {
    this.service.hookToQuery();
    this.canPermission = this.permission.getGrantedPolicy('FaceAPI.ScheduleFormats.Get');
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

  update(record: ScheduleFormatDto) {
    this.serviceDetail.update(record);
  }

  delete(record: ScheduleFormatDto) {
    this.service.delete(record);
  }
}
