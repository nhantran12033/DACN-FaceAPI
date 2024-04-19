import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { ScheduleDto } from '../schedules/models';
import type { ScheduleDetailDto } from '../schedule-details/models';
import type { ScheduleFormatDto } from '../schedule-formats/models';

export interface GetTimesheetsInput extends GetTimesheetsInputBase {
}

export interface GetTimesheetsInputBase extends PagedAndSortedResultRequestDto {
  filterText?: string;
  image?: string;
  code?: string;
  timeMin?: string;
  timeMax?: string;
  note?: string;
  scheduleId?: string;
  scheduleDetailId?: string;
  scheduleFormatId?: string;
}

export interface TimesheetCreateDto extends TimesheetCreateDtoBase {
}

export interface TimesheetCreateDtoBase {
  url?: string;
  image?: string;
  code?: string;
  time?: string;
  note?: string;
  scheduleId?: string;
  scheduleDetailId?: string;
  scheduleFormatId?: string;
}

export interface TimesheetDto extends TimesheetDtoBase {
}

export interface TimesheetDtoBase extends FullAuditedEntityDto<string> {
  image?: string;
  code?: string;
  time?: string;
  note?: string;
  scheduleId?: string;
  scheduleDetailId?: string;
  scheduleFormatId?: string;
  concurrencyStamp?: string;
}

export interface TimesheetExcelDownloadDto extends TimesheetExcelDownloadDtoBase {
}

export interface TimesheetExcelDownloadDtoBase {
  downloadToken?: string;
  filterText?: string;
  image?: string;
  code?: string;
  timeMin?: string;
  timeMax?: string;
  note?: string;
  scheduleId?: string;
  scheduleDetailId?: string;
  scheduleFormatId?: string;
}

export interface TimesheetUpdateDto extends TimesheetUpdateDtoBase {
}

export interface TimesheetUpdateDtoBase {
  image?: string;
  code?: string;
  time?: string;
  note?: string;
  scheduleId?: string;
  scheduleDetailId?: string;
  scheduleFormatId?: string;
  concurrencyStamp?: string;
}

export interface TimesheetWithNavigationPropertiesDto extends TimesheetWithNavigationPropertiesDtoBase {
}

export interface TimesheetWithNavigationPropertiesDtoBase {
  timesheet: TimesheetDto;
  schedule: ScheduleDto;
  scheduleDetail: ScheduleDetailDto;
  scheduleFormat: ScheduleFormatDto;
}
