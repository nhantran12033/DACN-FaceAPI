using System;

namespace FaceAPI.Salaries
{
    public abstract class SalaryExcelDtoBase
    {
        public string? Code { get; set; }
        public double Allowance { get; set; }
        public double Basic { get; set; }
        public double Bonus { get; set; }
        public double Total { get; set; }
    }
}