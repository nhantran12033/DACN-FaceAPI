import { inject, Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListService } from '@abp/ng.core';
import { finalize, tap } from 'rxjs/operators';
import type { ScheduleDetailWithNavigationPropertiesDto } from '../../../proxy/schedule-details/models';
import { ScheduleDetailService } from '../../../proxy/schedule-details/schedule-detail.service';

export abstract class AbstractScheduleDetailDetailViewService {
  protected readonly fb = inject(FormBuilder);
  public readonly proxyService = inject(ScheduleDetailService);
  public readonly list = inject(ListService);

  public readonly getScheduleFormatLookup = this.proxyService.getScheduleFormatLookup;

  isBusy = false;
  isVisible = false;
  selected = {} as any;
  form: FormGroup | undefined;

  buildForm() {
    const { name, fromDate, toDate, note } = this.selected?.scheduleDetail || {};

    const { scheduleFormats = [] } = this.selected || {};

    this.form = this.fb.group({
      name: [name ?? null, []],
      fromDate: [fromDate ? new Date(fromDate) : null, []],
      toDate: [toDate ? new Date(toDate) : null, []],
      note: [note ?? null, []],
      scheduleFormatIds: [scheduleFormats.map(({ id }) => id), []],
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

  update(record: ScheduleDetailWithNavigationPropertiesDto) {
    this.proxyService.getWithNavigationProperties(record.scheduleDetail.id).subscribe(data => {
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
      return this.proxyService.update(this.selected.scheduleDetail.id, {
        ...this.form.value,
        concurrencyStamp: this.selected.scheduleDetail.concurrencyStamp,
      });
    }
    return this.proxyService.create(this.form.value);
  }

  changeVisible($event: boolean) {
    this.isVisible = $event;
  }
}
