import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { DepartmentDto } from '../departments/models';
import type { ScheduleDetailDto } from '../schedule-details/models';

export interface GetSchedulesInput extends GetSchedulesInputBase {
}

export interface GetSchedulesInputBase extends PagedAndSortedResultRequestDto {
  filterText?: string;
  code?: string;
  name?: string;
  dateFromMin?: string;
  dateFromMax?: string;
  dateToMin?: string;
  dateToMax?: string;
  note?: string;
  departmentId?: string;
  scheduleDetailId?: string;
}

export interface ScheduleCreateDto extends ScheduleCreateDtoBase {
}

export interface ScheduleCreateDtoBase {
  code?: string;
  name?: string;
  dateFrom?: string;
  dateTo?: string;
  note?: string;
  departmentId?: string;
  scheduleDetailIds: string[];
}

export interface ScheduleDto extends ScheduleDtoBase {
}

export interface ScheduleDtoBase extends FullAuditedEntityDto<string> {
  code?: string;
  name?: string;
  dateFrom?: string;
  dateTo?: string;
  note?: string;
  departmentId?: string;
  concurrencyStamp?: string;
}

export interface ScheduleExcelDownloadDto extends ScheduleExcelDownloadDtoBase {
}

export interface ScheduleExcelDownloadDtoBase {
  downloadToken?: string;
  filterText?: string;
  code?: string;
  name?: string;
  dateFromMin?: string;
  dateFromMax?: string;
  dateToMin?: string;
  dateToMax?: string;
  note?: string;
  departmentId?: string;
  scheduleDetailId?: string;
}

export interface ScheduleUpdateDto extends ScheduleUpdateDtoBase {
}

export interface ScheduleUpdateDtoBase {
  code?: string;
  name?: string;
  dateFrom?: string;
  dateTo?: string;
  note?: string;
  departmentId?: string;
  scheduleDetailIds: string[];
  concurrencyStamp?: string;
}

export interface ScheduleWithNavigationPropertiesDto extends ScheduleWithNavigationPropertiesDtoBase {
}

export interface ScheduleWithNavigationPropertiesDtoBase {
  schedule: ScheduleDto;
  department: DepartmentDto;
  scheduleDetails: ScheduleDetailDto[];
}
