import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { TitleDto } from '../titles/models';

export interface DepartmentCreateDto extends DepartmentCreateDtoBase {
}

export interface DepartmentCreateDtoBase {
  code?: string;
  name?: string;
  date?: string;
  note?: string;
  titleIds: string[];
}

export interface DepartmentDto extends DepartmentDtoBase {
}

export interface DepartmentDtoBase extends FullAuditedEntityDto<string> {
  code?: string;
  name?: string;
  date?: string;
  note?: string;
  concurrencyStamp?: string;
}

export interface DepartmentUpdateDto extends DepartmentUpdateDtoBase {
}

export interface DepartmentUpdateDtoBase {
  code?: string;
  name?: string;
  date?: string;
  note?: string;
  titleIds: string[];
  concurrencyStamp?: string;
}

export interface DepartmentWithNavigationPropertiesDto extends DepartmentWithNavigationPropertiesDtoBase {
}

export interface DepartmentWithNavigationPropertiesDtoBase {
  department: DepartmentDto;
  titles: TitleDto[];
}

export interface GetDepartmentsInput extends GetDepartmentsInputBase {
}

export interface GetDepartmentsInputBase extends PagedAndSortedResultRequestDto {
  filterText?: string;
  code?: string;
  name?: string;
  dateMin?: string;
  dateMax?: string;
  note?: string;
  titleId?: string;
}
