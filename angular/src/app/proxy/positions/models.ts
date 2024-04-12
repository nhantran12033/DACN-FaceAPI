import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { DepartmentDto } from '../departments/models';

export interface GetPositionsInput extends GetPositionsInputBase {}

export interface GetPositionsInputBase extends PagedAndSortedResultRequestDto {
  filterText?: string;
  code?: string;
  name?: string;
  note?: string;
  departmentId?: string;
}

export interface PositionCreateDto extends PositionCreateDtoBase {}

export interface PositionCreateDtoBase {
  code?: string;
  name?: string;
  note?: string;
  departmentId?: string;
}

export interface PositionDto extends PositionDtoBase {}

export interface PositionDtoBase extends FullAuditedEntityDto<string> {
  code?: string;
  name?: string;
  note?: string;
  departmentId?: string;
  concurrencyStamp?: string;
}

export interface PositionUpdateDto extends PositionUpdateDtoBase {}

export interface PositionUpdateDtoBase {
  code?: string;
  name?: string;
  note?: string;
  departmentId?: string;
  concurrencyStamp?: string;
}

export interface PositionWithNavigationPropertiesDto
  extends PositionWithNavigationPropertiesDtoBase {}

export interface PositionWithNavigationPropertiesDtoBase {
  position: PositionDto;
  department: DepartmentDto;
}
