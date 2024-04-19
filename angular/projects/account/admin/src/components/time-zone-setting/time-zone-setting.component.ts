import { LocalizationModule, SubscriptionService, ConfigStateService } from '@abp/ng.core';
import { ThemeSharedModule, ToasterService } from '@abp/ng.theme.shared';
import { collapse } from '@abp/ng.theme.shared';
import { Component, inject} from '@angular/core';
import { TimeZoneSettingsService } from '@abp/ng.setting-management/proxy';
import { AsyncPipe, JsonPipe, NgFor, NgIf } from '@angular/common';
import { FormBuilder,ReactiveFormsModule } from '@angular/forms';
import { firstValueFrom} from 'rxjs';
import { TrackByService } from '@abp/ng.core';


@Component({
  standalone: true,
  selector: 'abp-time-zone-setting',
  templateUrl: 'time-zone-setting.component.html',
  animations: [collapse],
  imports: [LocalizationModule, JsonPipe, NgFor, NgIf, ReactiveFormsModule, ThemeSharedModule, AsyncPipe],
  providers: [SubscriptionService]
})
export class TimeZoneSettingComponent{
  protected readonly timeZoneSettingsService = inject(TimeZoneSettingsService);
  protected readonly subscription = inject(SubscriptionService)
  protected readonly toasterService = inject(ToasterService)
  protected readonly configState = inject(ConfigStateService)
  protected readonly fb = inject(FormBuilder)
  public readonly track = inject(TrackByService)
  timezoneForm = this.fb.group({
    selectedTimeZone: ['UTC']
  })
  
  loading = false;
  constructor(){
    this.init()
  }
  
  async init(){
    this.selectedTimeZone = await firstValueFrom(this.timeZoneSettingsService.get());
    this._timeZones = this.timeZoneSettingsService.getTimezones()
  }

  private _timeZones
  get timeZones(){
    return this._timeZones
  }

  get selectedTimeZone(){
    return this.timezoneForm.value.selectedTimeZone
  }
  set selectedTimeZone(val: string){
    this.timezoneForm.setValue({ selectedTimeZone:val })
  }

  async onSubmit(){
    const value = this.selectedTimeZone
    this.loading = true
    await firstValueFrom(this.timeZoneSettingsService.update(value))
    this.loading = false
    this.toasterService.success('AbpSettingManagement::SuccessfullySaved');
  }
}
