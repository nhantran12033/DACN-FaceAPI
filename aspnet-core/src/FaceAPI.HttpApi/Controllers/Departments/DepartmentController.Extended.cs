using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using FaceAPI.Departments;

namespace FaceAPI.Controllers.Departments
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Department")]
    [Route("api/app/departments")]

    public class DepartmentController : DepartmentControllerBase, IDepartmentsAppService
    {
        public DepartmentController(IDepartmentsAppService departmentsAppService) : base(departmentsAppService)
        {
        }
    }
}