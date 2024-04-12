import { ListService, TrackByService } from '@abp/ng.core';
import { Component, OnInit, inject } from '@angular/core';

import type { SalaryWithNavigationPropertiesDto } from '../../../proxy/salaries/models';
import { SalaryViewService } from '../services/salary.service';
import { SalaryDetailViewService } from '../services/salary-detail.service';

@Component({
  template: '',
})
export abstract class AbstractSalaryComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(SalaryViewService);
  public readonly serviceDetail = inject(SalaryDetailViewService);
  protected title = '::Salaries';

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

  update(record: SalaryWithNavigationPropertiesDto) {
    this.serviceDetail.update(record);
  }

  delete(record: SalaryWithNavigationPropertiesDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }
  getSalaryData(id: string) {
    this.service.getSalary(id);
  }
}
