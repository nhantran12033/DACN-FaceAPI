using FaceAPI.Departments;
using FaceAPI.Titles;
using FaceAPI.Departments;
using FaceAPI.Titles;
using FaceAPI.Departments;
using FaceAPI.Titles;
using FaceAPI.Departments;
using FaceAPI.Titles;
using FaceAPI.Departments;
using FaceAPI.Titles;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace FaceAPI.Staffs
{
    public class Staff : StaffBase
    {
        //<suite-custom-code-autogenerated>
        protected Staff()
        {

        }

        public Staff(Guid id, Guid? departmentId, Guid? titleId, DateTime birthday, DateTime startWork, int debt, string? image = null, string? code = null, string? name = null, string? sex = null, string? phone = null, string? email = null, string? address = null, string? note = null)
            : base(id, departmentId, titleId, birthday, startWork, debt, image, code, name, sex, phone, email, address, note)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}