import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { DepartmentDto } from '../departments/models';
import type { TitleDto } from '../titles/models';
import type { TimesheetDto } from '../timesheets/models';

export interface GetStaffsInput extends GetStaffsInputBase {
}

export interface GetStaffsInputBase extends PagedAndSortedResultRequestDto {
  filterText?: string;
  image?: string;
  code?: string;
  name?: string;
  sex?: string;
  birthdayMin?: string;
  birthdayMax?: string;
  startWorkMin?: string;
  startWorkMax?: string;
  phone?: string;
  email?: string;
  address?: string;
  debtMin?: number;
  debtMax?: number;
  note?: string;
  departmentId?: string;
  titleId?: string;
  timesheetId?: string;
}

export interface StaffCreateDto extends StaffCreateDtoBase {
}

export interface StaffCreateDtoBase {
  image?: string;
  code?: string;
  name?: string;
  sex?: string;
  birthday?: string;
  startWork?: string;
  phone?: string;
  email?: string;
  address?: string;
  debt: number;
  note?: string;
  departmentId?: string;
  titleId?: string;
  timesheetIds: string[];
}

export interface StaffDto extends StaffDtoBase {
}

export interface StaffDtoBase extends FullAuditedEntityDto<string> {
  image?: string;
  code?: string;
  name?: string;
  sex?: string;
  birthday?: string;
  startWork?: string;
  phone?: string;
  email?: string;
  address?: string;
  debt: number;
  note?: string;
  departmentId?: string;
  titleId?: string;
  concurrencyStamp?: string;
}

export interface StaffExcelDownloadDto extends StaffExcelDownloadDtoBase {
}

export interface StaffExcelDownloadDtoBase {
  downloadToken?: string;
  filterText?: string;
  image?: string;
  code?: string;
  name?: string;
  sex?: string;
  birthdayMin?: string;
  birthdayMax?: string;
  startWorkMin?: string;
  startWorkMax?: string;
  phone?: string;
  email?: string;
  address?: string;
  debtMin?: number;
  debtMax?: number;
  note?: string;
  departmentId?: string;
  titleId?: string;
  timesheetId?: string;
}

export interface StaffUpdateDto extends StaffUpdateDtoBase {
}

export interface StaffUpdateDtoBase {
  image?: string;
  code?: string;
  name?: string;
  sex?: string;
  birthday?: string;
  startWork?: string;
  phone?: string;
  email?: string;
  address?: string;
  debt: number;
  note?: string;
  departmentId?: string;
  titleId?: string;
  timesheetIds: string[];
  concurrencyStamp?: string;
}

export interface StaffWithNavigationPropertiesDto extends StaffWithNavigationPropertiesDtoBase {
}

export interface StaffWithNavigationPropertiesDtoBase {
  staff: StaffDto;
  department: DepartmentDto;
  title: TitleDto;
  timesheets: TimesheetDto[];
}
