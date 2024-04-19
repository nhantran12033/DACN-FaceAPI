import { ListService, TrackByService } from '@abp/ng.core';
import { Component, OnInit, inject } from '@angular/core';

import type { TimesheetWithNavigationPropertiesDto } from '../../../proxy/timesheets/models';
import { TimesheetViewService } from '../services/timesheet.service';
import { TimesheetDetailViewService } from '../services/timesheet-detail.service';
import { WebcamImage } from 'ngx-webcam';

@Component({
  template: '',
})
export abstract class AbstractTimesheetComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(TimesheetViewService);
  public readonly serviceDetail = inject(TimesheetDetailViewService);
  protected title = '::Timesheets';

  ngOnInit() {
    this.service.hookToQuery();
  }
  public webcamImage: WebcamImage = null;

  handleImage(webcamImage: WebcamImage) {
    this.webcamImage = webcamImage;
    this.service.createDto(webcamImage.imageAsDataUrl);
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

  update(record: TimesheetWithNavigationPropertiesDto) {
    this.serviceDetail.update(record);
  }

  delete(record: TimesheetWithNavigationPropertiesDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }
}
