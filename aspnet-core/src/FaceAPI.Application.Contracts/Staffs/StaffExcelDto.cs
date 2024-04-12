using System;

namespace FaceAPI.Staffs
{
    public abstract class StaffExcelDtoBase
    {
        public string? Image { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Sex { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime StartWork { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int Debt { get; set; }
        public string? Note { get; set; }
    }
}