import { inject, Injectable, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ListService } from '@abp/ng.core';
import { finalize, tap } from 'rxjs/operators';
import type { ScheduleWithNavigationPropertiesDto } from '../../../proxy/schedules/models';
import { ScheduleService } from '../../../proxy/schedules/schedule.service';
export abstract class AbstractScheduleDetailViewService {
  protected readonly fb = inject(FormBuilder);
  public readonly proxyService = inject(ScheduleService);
  public readonly list = inject(ListService);
  public readonly getScheduleDetailLookup = this.proxyService.getScheduleDetailLookup;

  public readonly getStaffLookup = this.proxyService.getStaffLookup;
  isOpenDetail = false;
  isOpenFormatDetail = false;
  isBusy = false;
  isVisible = false;
  selected = {} as any;
  form: FormGroup | undefined;
  dataScheduleDto: ScheduleWithNavigationPropertiesDto;
    expandedRowId: any;
  buildForm() {
    const { code, name, dateFrom, dateTo, note, staffId } = this.selected?.schedule || {};

    const { scheduleDetails = [] } = this.selected || {};

    this.form = this.fb.group({
      code: [code ?? null, []],
      name: [name ?? null, []],
      dateFrom: [dateFrom ? new Date(dateFrom) : null, []],
      dateTo: [dateTo ? new Date(dateTo) : null, []],
      note: [note ?? null, []],
      staffId: [staffId ?? null, [Validators.required]],
      scheduleDetailIds: [scheduleDetails.map(({ id }) => id), []],
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

  update(record: ScheduleWithNavigationPropertiesDto) {
    this.proxyService.getWithNavigationProperties(record.schedule.id).subscribe(data => {
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
  getData(id) {
    this.isOpenDetail = true;
    this.proxyService.getWithNavigationProperties(id).subscribe(result => {
      this.dataScheduleDto = result;

      
    })
  }
  getSchedule() {
    return this.dataScheduleDto;
  }
  private createRequest() {
    if (this.selected) {
      return this.proxyService.update(this.selected.schedule.id, {
        ...this.form.value,
        concurrencyStamp: this.selected.schedule.concurrencyStamp,
      });
    }
    return this.proxyService.create(this.form.value);
  }

  changeVisible($event: boolean) {
    this.isVisible = $event;
  }

}
