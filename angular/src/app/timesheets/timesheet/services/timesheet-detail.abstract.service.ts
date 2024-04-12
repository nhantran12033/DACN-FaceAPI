import { inject, Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListService } from '@abp/ng.core';
import { finalize, tap } from 'rxjs/operators';
import type { TimesheetWithNavigationPropertiesDto } from '../../../proxy/timesheets/models';
import { TimesheetService } from '../../../proxy/timesheets/timesheet.service';

export abstract class AbstractTimesheetDetailViewService {
  protected readonly fb = inject(FormBuilder);
  public readonly proxyService = inject(TimesheetService);
  public readonly list = inject(ListService);

  public readonly getScheduleLookup = this.proxyService.getScheduleLookup;

  public readonly getScheduleDetailLookup = this.proxyService.getScheduleDetailLookup;

  public readonly getScheduleFormatLookup = this.proxyService.getScheduleFormatLookup;

  isBusy = false;
  isVisible = false;
  selected = {} as any;
  form: FormGroup | undefined;

  buildForm() {
    const {
      code,
      timeIn,
      timeOut,
      hoursWork,
      note,
      scheduleId,
      scheduleDetailId,
      scheduleFormatId,
    } = this.selected?.timesheet || {};

    this.form = this.fb.group({
      code: [code ?? null, []],
      timeIn: [timeIn ? new Date(timeIn) : null, []],
      timeOut: [timeOut ? new Date(timeOut) : null, []],
      hoursWork: [hoursWork ?? null, []],
      note: [note ?? null, []],
      scheduleId: [scheduleId ?? null, []],
      scheduleDetailId: [scheduleDetailId ?? null, []],
      scheduleFormatId: [scheduleFormatId ?? null, []],
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

  update(record: TimesheetWithNavigationPropertiesDto) {
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
      return this.proxyService.update(this.selected.timesheet.id, {
        ...this.form.value,
        concurrencyStamp: this.selected.timesheet.concurrencyStamp,
      });
    }
    return this.proxyService.create(this.form.value);
  }

  changeVisible($event: boolean) {
    this.isVisible = $event;
  }
}
