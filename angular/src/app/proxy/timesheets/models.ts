import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { ScheduleDto } from '../schedules/models';
import type { ScheduleDetailDto } from '../schedule-details/models';
import type { ScheduleFormatDto } from '../schedule-formats/models';

export interface GetTimesheetsInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  code?: string;
  timeInMin?: string;
  timeInMax?: string;
  timeOutMin?: string;
  timeOutMax?: string;
  hoursWorkMin?: number;
  hoursWorkMax?: number;
  note?: string;
  scheduleId?: string;
  scheduleDetailId?: string;
  scheduleFormatId?: string;
}

export interface TimesheetCreateDto {
  code?: string;
  timeIn?: string;
  timeOut?: string;
  hoursWork?: number;
  note?: string;
  scheduleId?: string;
  scheduleDetailId?: string;
  scheduleFormatId?: string;
}

export interface TimesheetDto extends FullAuditedEntityDto<string> {
  code?: string;
  timeIn?: string;
  timeOut?: string;
  hoursWork?: number;
  note?: string;
  scheduleId?: string;
  scheduleDetailId?: string;
  scheduleFormatId?: string;
  concurrencyStamp?: string;
}

export interface TimesheetExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}

export interface TimesheetUpdateDto {
  code?: string;
  timeIn?: string;
  timeOut?: string;
  hoursWork?: number;
  note?: string;
  scheduleId?: string;
  scheduleDetailId?: string;
  scheduleFormatId?: string;
  concurrencyStamp?: string;
}

export interface TimesheetWithNavigationPropertiesDto {
  timesheet: TimesheetDto;
  schedule: ScheduleDto;
  scheduleDetail: ScheduleDetailDto;
  scheduleFormat: ScheduleFormatDto;
}
