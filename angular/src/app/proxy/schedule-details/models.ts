import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface GetScheduleDetailsInput extends GetScheduleDetailsInputBase {
}

export interface GetScheduleDetailsInputBase extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
  fromMin?: string;
  fromMax?: string;
  toMin?: string;
  toMax?: string;
  note?: string;
}

export interface ScheduleDetailCreateDto extends ScheduleDetailCreateDtoBase {
}

export interface ScheduleDetailCreateDtoBase {
  name?: string;
  from?: string;
  to?: string;
  note?: string;
}

export interface ScheduleDetailDto extends ScheduleDetailDtoBase {
}

export interface ScheduleDetailDtoBase extends FullAuditedEntityDto<string> {
  name?: string;
  from?: string;
  to?: string;
  note?: string;
  concurrencyStamp?: string;
}

export interface ScheduleDetailUpdateDto extends ScheduleDetailUpdateDtoBase {
}

export interface ScheduleDetailUpdateDtoBase {
  name?: string;
  from?: string;
  to?: string;
  note?: string;
  concurrencyStamp?: string;
}
