using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using FaceAPI.Staffs;

namespace FaceAPI.Controllers.Staffs
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Staff")]
    [Route("api/app/staffs")]

    public class StaffController : StaffControllerBase, IStaffsAppService
    {
        public StaffController(IStaffsAppService staffsAppService) : base(staffsAppService)
        {
        }
    }
}