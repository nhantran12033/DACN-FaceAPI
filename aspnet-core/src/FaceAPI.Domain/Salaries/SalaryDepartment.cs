using System;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.Salaries
{
    public class SalaryDepartment : Entity
    {

        public Guid SalaryId { get; protected set; }

        public Guid DepartmentId { get; protected set; }

        private SalaryDepartment()
        {

        }

        public SalaryDepartment(Guid salaryId, Guid departmentId)
        {
            SalaryId = salaryId;
            DepartmentId = departmentId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    SalaryId,
                    DepartmentId
                };
        }
    }
}