import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { ScheduleFormatDto } from '../schedule-formats/models';

export interface GetScheduleDetailsInput extends GetScheduleDetailsInputBase {}

export interface GetScheduleDetailsInputBase extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
  fromDateMin?: string;
  fromDateMax?: string;
  toDateMin?: string;
  toDateMax?: string;
  note?: string;
  scheduleFormatId?: string;
}

export interface ScheduleDetailCreateDto extends ScheduleDetailCreateDtoBase {}

export interface ScheduleDetailCreateDtoBase {
  name?: string;
  fromDate?: string;
  toDate?: string;
  note?: string;
  scheduleFormatIds: string[];
}

export interface ScheduleDetailDto extends ScheduleDetailDtoBase {}

export interface ScheduleDetailDtoBase extends FullAuditedEntityDto<string> {
  name?: string;
  fromDate?: string;
  toDate?: string;
  note?: string;
  concurrencyStamp?: string;
}

export interface ScheduleDetailUpdateDto extends ScheduleDetailUpdateDtoBase {}

export interface ScheduleDetailUpdateDtoBase {
  name?: string;
  fromDate?: string;
  toDate?: string;
  note?: string;
  scheduleFormatIds: string[];
  concurrencyStamp?: string;
}

export interface ScheduleDetailWithNavigationPropertiesDto
  extends ScheduleDetailWithNavigationPropertiesDtoBase {}

export interface ScheduleDetailWithNavigationPropertiesDtoBase {
  scheduleDetail: ScheduleDetailDto;
  scheduleFormats: ScheduleFormatDto[];
}
