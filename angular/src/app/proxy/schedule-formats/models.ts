import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface GetScheduleFormatsInput extends GetScheduleFormatsInputBase {
}

export interface GetScheduleFormatsInputBase extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
  dateMin?: string;
  dateMax?: string;
  fromHoursMin?: number;
  fromHoursMax?: number;
  toHoursMin?: number;
  toHoursMax?: number;
  note?: string;
}

export interface ScheduleFormatCreateDto extends ScheduleFormatCreateDtoBase {
}

export interface ScheduleFormatCreateDtoBase {
  name?: string;
  date?: string;
  fromHours: number;
  toHours: number;
  note?: string;
}

export interface ScheduleFormatDto extends ScheduleFormatDtoBase {
}

export interface ScheduleFormatDtoBase extends FullAuditedEntityDto<string> {
  name?: string;
  date?: string;
  fromHours: number;
  toHours: number;
  note?: string;
  concurrencyStamp?: string;
}

export interface ScheduleFormatUpdateDto extends ScheduleFormatUpdateDtoBase {
}

export interface ScheduleFormatUpdateDtoBase {
  name?: string;
  date?: string;
  fromHours: number;
  toHours: number;
  note?: string;
  concurrencyStamp?: string;
}
