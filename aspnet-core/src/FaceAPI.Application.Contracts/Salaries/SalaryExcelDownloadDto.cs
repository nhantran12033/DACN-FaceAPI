using Volo.Abp.Application.Dtos;
using System;

namespace FaceAPI.Salaries
{
    public abstract class SalaryExcelDownloadDtoBase
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? Code { get; set; }
        public double? AllowanceMin { get; set; }
        public double? AllowanceMax { get; set; }
        public double? BasicMin { get; set; }
        public double? BasicMax { get; set; }
        public double? BonusMin { get; set; }
        public double? BonusMax { get; set; }
        public double? TotalMin { get; set; }
        public double? TotalMax { get; set; }
        public Guid? DepartmentId { get; set; }

        public SalaryExcelDownloadDtoBase()
        {

        }
    }
}