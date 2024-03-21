import { ListService, TrackByService } from '@abp/ng.core';
import { Component, OnInit, inject } from '@angular/core';

import type { DepartmentWithNavigationPropertiesDto } from '../../../proxy/departments/models';
import { DepartmentViewService } from '../services/department.service';
import { DepartmentDetailViewService } from '../services/department-detail.service';

@Component({
  template: '',
})
export abstract class AbstractDepartmentComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(DepartmentViewService);
  public readonly serviceDetail = inject(DepartmentDetailViewService);
  protected title = '::Departments';

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

  update(record: DepartmentWithNavigationPropertiesDto) {
    this.serviceDetail.update(record);
  }

  delete(record: DepartmentWithNavigationPropertiesDto) {
    this.service.delete(record);
  }
}
