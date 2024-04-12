import { ConfigStateService, ListService, TrackByService } from '@abp/ng.core';
import { Component, OnInit, inject } from '@angular/core';

import type { StaffWithNavigationPropertiesDto } from '../../../proxy/staffs/models';
import { StaffViewService } from '../services/staff.service';
import { StaffDetailViewService } from '../services/staff-detail.service';

@Component({
  template: '',
})
export abstract class AbstractStaffComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(StaffViewService);
  public readonly serviceDetail = inject(StaffDetailViewService);

  protected title = '::Staffs';

  ngOnInit() {
    this.service.hookToQuery();
    this.getUserData();
    this.getTotal();
    
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

  update(record: StaffWithNavigationPropertiesDto) {
    this.serviceDetail.update(record);
  }

  delete(record: StaffWithNavigationPropertiesDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }
  getData(id) {
    this.serviceDetail.getData(id);
  }
  getTotal() {
    this.serviceDetail.getTotalSalary();
  }
  getUserData() {
    this.serviceDetail.getUserData();
  }
}
