import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface GetTitlesInput extends GetTitlesInputBase {}

export interface GetTitlesInputBase extends PagedAndSortedResultRequestDto {
  filterText?: string;
  code?: string;
  name?: string;
  note?: string;
}

export interface TitleCreateDto extends TitleCreateDtoBase {}

export interface TitleCreateDtoBase {
  code?: string;
  name?: string;
  note?: string;
}

export interface TitleDto extends TitleDtoBase {}

export interface TitleDtoBase extends FullAuditedEntityDto<string> {
  code?: string;
  name?: string;
  note?: string;
  concurrencyStamp?: string;
}

export interface TitleUpdateDto extends TitleUpdateDtoBase {}

export interface TitleUpdateDtoBase {
  code?: string;
  name?: string;
  note?: string;
  concurrencyStamp?: string;
}
