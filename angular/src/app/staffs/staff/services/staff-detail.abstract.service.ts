import { inject, Injectable, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ConfigStateService, ListService } from '@abp/ng.core';
import { finalize, tap } from 'rxjs/operators';
import type { StaffWithNavigationPropertiesDto } from '../../../proxy/staffs/models';
import { StaffService } from '../../../proxy/staffs/staff.service';
import { DepartmentService, DepartmentWithNavigationPropertiesDto } from '../../../proxy/departments';
import { TitleDto } from '../../../proxy/titles';
import { SalaryDto, SalaryService } from '../../../proxy/salaries';

export abstract class AbstractStaffDetailViewService {

  protected readonly fb = inject(FormBuilder);
  public readonly proxyService = inject(StaffService);
  public readonly proxyServiceDepartment = inject(DepartmentService);
  public readonly proxyServiceSalary = inject(SalaryService);
  public readonly list = inject(ListService);
  public readonly getTimesheetLookup = this.proxyService.getTimesheetLookup;
  public config = inject(ConfigStateService)
  public readonly getDepartmentLookup = this.proxyService.getDepartmentLookup;

  public readonly getTitleLookup = this.proxyService.getTitleLookup;
  isOpenDetail = false;
  isBusy = false;
  isVisible = false;
  selected = {} as any;
  form: FormGroup | undefined;
  dataDto: StaffWithNavigationPropertiesDto;
  dataUserDto: StaffWithNavigationPropertiesDto;
  titleDto: TitleDto[];
  salaryDto: SalaryDto;
  buildForm() {
    const {
      image,
      code,
      name,
      sex,
      birthday,
      startWork,
      phone,
      email,
      address,
      debt,
      note,
      departmentId,
      titleId,
    } = this.selected?.staff || {};

    const { timesheets = [] } = this.selected || {};

    this.form = this.fb.group({
      image: [image ?? null, []],
      code: [code ?? null, []],
      name: [name ?? null, []],
      sex: [sex ?? null, []],
      birthday: [birthday ? new Date(birthday) : null, []],
      startWork: [startWork ? new Date(startWork) : null, []],
      phone: [phone ?? null, []],
      email: [email ?? null, [Validators.email]],
      address: [address ?? null, []],
      debt: [debt ?? null, []],
      note: [note ?? null, []],
      departmentId: [departmentId ?? null, []],
      titleId: [titleId ?? null, []],
      timesheetIds: [timesheets.map(({ id }) => id), []],
    });
  }

  showForm() {
    this.buildForm();
    this.isVisible = true;
  }

  create() {
    this.selected = undefined;
    this.showForm();
  }

  update(record: StaffWithNavigationPropertiesDto) {
    this.proxyService.getWithNavigationProperties(record.staff.id).subscribe(data => {
      this.selected = data;
      this.showForm();
    });
  }

  hideForm() {
    this.isVisible = false;
    this.form.reset();
  }

  submitForm() {
    if (this.form.invalid) return;

    this.isBusy = true;

    const request = this.createRequest().pipe(
      finalize(() => (this.isBusy = false)),
      tap(() => this.hideForm())
    );

    request.subscribe(this.list.get);
  }

  private createRequest() {
    if (this.selected) {
      return this.proxyService.update(this.selected.staff.id, {
        ...this.form.value,
        concurrencyStamp: this.selected.staff.concurrencyStamp,
      });
    }
    return this.proxyService.create(this.form.value);
  }

  changeVisible($event: boolean) {
    this.isVisible = $event;
  }
  getUserData() {
    this.config.getOne$("currentUser").subscribe(currentUser => {
      var user = currentUser.userName;
        this.proxyService.getWithNavigationCodeProperties(user).subscribe(result => {
          this.dataUserDto = result;
        })  
    })
  }
  getData(id) {
    this.isOpenDetail = true;
    this.proxyService.getWithNavigationProperties(id).subscribe(result => {
      this.dataDto = result;
    })
  }
  getTitle() {
    if (this.form.get('departmentId').value == null) {
      return;
    }
    else {
      this.proxyServiceDepartment.getWithNavigationProperties(this.form.get('departmentId').value).subscribe(result => {
        this.titleDto = result.titles
      })
    }
    
  }
  getTotalSalary() {
    if (this.form.get('departmentId').value != null && this.form.get('titleId').value != null) {
      return;
    }
    else if (this.form.get('departmentId').value != null && this.form.get('titleId').value != null) {
      this.proxyServiceSalary.getWithNavigationProperties(this.form.get('departmentId').value, this.form.get('titleId').value).subscribe(result => {
        if (result != null) {
          this.salaryDto = result.salary
        }
        else {
          this.salaryDto.total = 0
        }
      })
    }
    else {
      return;
    }

  }
  onImageSelected(event: Event): void {
    const file = (event.target as HTMLInputElement).files[0];
    if (file) {
      this.form.get('image').setValue(file.name);
    }
  }
}
