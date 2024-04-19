import { inject, Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListService } from '@abp/ng.core';
import { finalize, tap } from 'rxjs/operators';
import type { ScheduleFormatDto } from '../../../proxy/schedule-formats/models';
import { ScheduleFormatService } from '../../../proxy/schedule-formats/schedule-format.service';

export abstract class AbstractScheduleFormatDetailViewService {
  protected readonly fb = inject(FormBuilder);
  public readonly proxyService = inject(ScheduleFormatService);
  public readonly list = inject(ListService);

  isBusy = false;
  isVisible = false;
  selected = {} as any;
  form: FormGroup | undefined;

  buildForm() {
    const { name, date, fromHours, toHours, note } = this.selected || {};

    this.form = this.fb.group({
      name: [name ?? null, []],
      date: [date ? new Date(date) : null, []],
      fromHours: [fromHours ?? null, []],
      toHours: [toHours ?? null, []],
      note: [note ?? null, []],
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

  update(record: ScheduleFormatDto) {
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
      return this.proxyService.update(this.selected.id, {
        ...this.form.value,
        concurrencyStamp: this.selected.concurrencyStamp,
      });
    }
    return this.proxyService.create(this.form.value);
  }

  changeVisible($event: boolean) {
    this.isVisible = $event;
  }
}
