import { inject, Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListService } from '@abp/ng.core';
import { finalize, tap } from 'rxjs/operators';
import type { DepartmentWithNavigationPropertiesDto } from '../../../proxy/departments/models';
import { DepartmentService } from '../../../proxy/departments/department.service';

export abstract class AbstractDepartmentDetailViewService {
  protected readonly fb = inject(FormBuilder);
  public readonly proxyService = inject(DepartmentService);
  public readonly list = inject(ListService);

  public readonly getTitleLookup = this.proxyService.getTitleLookup;

  isBusy = false;
  isVisible = false;
  selected = {} as any;
  form: FormGroup | undefined;

  buildForm() {
    const { code, name, date, note } = this.selected?.department || {};

    const { titles = [] } = this.selected || {};

    this.form = this.fb.group({
      code: [code ?? null, []],
      name: [name ?? null, []],
      date: [date ? new Date(date) : null, []],
      note: [note ?? null, []],
      titleIds: [titles.map(({ id }) => id), []],
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

  update(record: DepartmentWithNavigationPropertiesDto) {
    this.proxyService.getWithNavigationProperties(record.department.id).subscribe(data => {
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
      return this.proxyService.update(this.selected.department.id, {
        ...this.form.value,
        concurrencyStamp: this.selected.department.concurrencyStamp,
      });
    }
    return this.proxyService.create(this.form.value);
  }

  changeVisible($event: boolean) {
    this.isVisible = $event;
  }
}
