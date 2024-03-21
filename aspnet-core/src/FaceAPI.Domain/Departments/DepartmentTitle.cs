using System;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.Departments
{
    public class DepartmentTitle : Entity
    {

        public Guid DepartmentId { get; protected set; }

        public Guid TitleId { get; protected set; }

        private DepartmentTitle()
        {

        }

        public DepartmentTitle(Guid departmentId, Guid titleId)
        {
            DepartmentId = departmentId;
            TitleId = titleId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    DepartmentId,
                    TitleId
                };
        }
    }
}