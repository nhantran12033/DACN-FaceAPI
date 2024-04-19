import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { DepartmentDto } from '../departments/models';
import type { TitleDto } from '../titles/models';

export interface GetSalariesInput extends GetSalariesInputBase {
}

export interface GetSalariesInputBase extends PagedAndSortedResultRequestDto {
  filterText?: string;
  code?: string;
  allowanceMin?: number;
  allowanceMax?: number;
  basicMin?: number;
  basicMax?: number;
  bonusMin?: number;
  bonusMax?: number;
  totalMin?: number;
  totalMax?: number;
  departmentId?: string;
  titleId?: string;
}

export interface SalaryCreateDto extends SalaryCreateDtoBase {
}

export interface SalaryCreateDtoBase {
  code?: string;
  allowance: number;
  basic: number;
  bonus: number;
  total: number;
  departmentId?: string;
  titleId?: string;
}

export interface SalaryDto extends SalaryDtoBase {
}

export interface SalaryDtoBase extends FullAuditedEntityDto<string> {
  code?: string;
  allowance: number;
  basic: number;
  bonus: number;
  total: number;
  departmentId?: string;
  titleId?: string;
  concurrencyStamp?: string;
}

export interface SalaryExcelDownloadDto extends SalaryExcelDownloadDtoBase {
}

export interface SalaryExcelDownloadDtoBase {
  downloadToken?: string;
  filterText?: string;
  code?: string;
  allowanceMin?: number;
  allowanceMax?: number;
  basicMin?: number;
  basicMax?: number;
  bonusMin?: number;
  bonusMax?: number;
  totalMin?: number;
  totalMax?: number;
  departmentId?: string;
  titleId?: string;
}

export interface SalaryUpdateDto extends SalaryUpdateDtoBase {
}

export interface SalaryUpdateDtoBase {
  code?: string;
  allowance: number;
  basic: number;
  bonus: number;
  total: number;
  departmentId?: string;
  titleId?: string;
  concurrencyStamp?: string;
}

export interface SalaryWithNavigationPropertiesDto extends SalaryWithNavigationPropertiesDtoBase {
}

export interface SalaryWithNavigationPropertiesDtoBase {
  salary: SalaryDto;
  department: DepartmentDto;
  title: TitleDto;
}
