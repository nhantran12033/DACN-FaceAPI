import { inject, Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListService } from '@abp/ng.core';
import { finalize, tap } from 'rxjs/operators';
import type { SalaryWithNavigationPropertiesDto } from '../../../proxy/salaries/models';
import { SalaryService } from '../../../proxy/salaries/salary.service';

export abstract class AbstractSalaryDetailViewService {
  protected readonly fb = inject(FormBuilder);
  public readonly proxyService = inject(SalaryService);
  public readonly list = inject(ListService);

  public readonly getDepartmentLookup = this.proxyService.getDepartmentLookup;

  public readonly getTitleLookup = this.proxyService.getTitleLookup;

  isBusy = false;
  isVisible = false;
  selected = {} as any;
  form: FormGroup | undefined;

  buildForm() {
    const { code, allowance, basic, bonus, total, departmentId, titleId } =
      this.selected?.salary || {};

    this.form = this.fb.group({
      code: [code ?? null, []],
      allowance: [allowance ?? null, []],
      basic: [basic ?? null, []],
      bonus: [bonus ?? null, []],
      total: [total ?? null, []],
      departmentId: [departmentId ?? null, []],
      titleId: [titleId ?? null, []],
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

  update(record: SalaryWithNavigationPropertiesDto) {
    this.selected = record;
    this.showForm();
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
      return this.proxyService.update(this.selected.salary.id, {
        ...this.form.value,
        concurrencyStamp: this.selected.salary.concurrencyStamp,
      });
    }
    return this.proxyService.create(this.form.value);
  }

  changeVisible($event: boolean) {
    this.isVisible = $event;
  }
}
