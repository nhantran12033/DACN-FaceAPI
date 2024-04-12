import { Injectable, inject } from '@angular/core';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ABP, downloadBlob, ListService, PagedResultDto } from '@abp/ng.core';
import { filter, switchMap, finalize } from 'rxjs/operators';
import type {
  GetSalariesInput,
  SalaryDto,
  SalaryWithNavigationPropertiesDto,
} from '../../../proxy/salaries/models';
import { SalaryService } from '../../../proxy/salaries/salary.service';
import { DepartmentDto, DepartmentService, DepartmentWithNavigationPropertiesDto } from '../../../proxy/departments';
import { TitleDto, TitleService } from '../../../proxy/titles';

export abstract class AbstractSalaryViewService {
  protected readonly proxyService = inject(SalaryService);
  protected readonly confirmationService = inject(ConfirmationService);
  protected readonly list = inject(ListService);
  protected readonly proxyServiceDepartment = inject(DepartmentService);
  protected readonly proxyServiceTitle = inject(TitleService);
  isExportToExcelBusy = false;
  isOpenDetail = false;
  data: PagedResultDto<SalaryWithNavigationPropertiesDto> = {
    items: [],
    totalCount: 0,
  };
  dataDto: SalaryDto;
  dataDetailDepartment: DepartmentDto;
  dataDetailTitle: TitleDto;
  filters = {} as GetSalariesInput;

  delete(record: SalaryWithNavigationPropertiesDto) {
    this.confirmationService
      .warn('::DeleteConfirmationMessage', '::AreYouSure', { messageLocalizationParams: [] })
      .pipe(
        filter(status => status === Confirmation.Status.confirm),
        switchMap(() => this.proxyService.delete(record.salary.id))
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

    const setData = (list: PagedResultDto<SalaryWithNavigationPropertiesDto>) => (this.data = list);

    this.list.hookToQuery(getData).subscribe(setData);
  }

  clearFilters() {
    this.filters = {} as GetSalariesInput;
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
        downloadBlob(result, 'Salary.xlsx');
      });
  }
  getSalary(id: string) {
    this.isOpenDetail = true;
    this.proxyService.get(id).subscribe(result => {
      this.dataDto = result
      this.proxyServiceDepartment.get(this.dataDto.departmentId).subscribe(department => {
        this.dataDetailDepartment = department;
      })
      this.proxyServiceTitle.get(this.dataDto.titleId).subscribe(title => {
        this.dataDetailTitle = title;
      })
    })
  }
}
