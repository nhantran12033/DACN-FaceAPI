using FaceAPI.Titles;
using FaceAPI.Titles;
using FaceAPI.Titles;
using System;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;

namespace FaceAPI.Departments
{
    public class DepartmentManager : DepartmentManagerBase
    {
        //<suite-custom-code-autogenerated>
        public DepartmentManager(IDepartmentRepository departmentRepository,
        IRepository<Title, Guid> titleRepository)
            : base(departmentRepository, titleRepository)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}