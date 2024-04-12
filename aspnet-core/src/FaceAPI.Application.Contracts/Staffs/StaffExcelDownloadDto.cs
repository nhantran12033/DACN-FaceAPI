using Volo.Abp.Application.Dtos;
using System;

namespace FaceAPI.Staffs
{
    public abstract class StaffExcelDownloadDtoBase
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? Image { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Sex { get; set; }
        public DateTime? BirthdayMin { get; set; }
        public DateTime? BirthdayMax { get; set; }
        public DateTime? StartWorkMin { get; set; }
        public DateTime? StartWorkMax { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int? DebtMin { get; set; }
        public int? DebtMax { get; set; }
        public string? Note { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? TitleId { get; set; }
        public Guid? TimesheetId { get; set; }

        public StaffExcelDownloadDtoBase()
        {

        }
    }
}