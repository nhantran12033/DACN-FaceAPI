using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using FaceAPI.Schedules;

namespace FaceAPI.Controllers.Schedules
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Schedule")]
    [Route("api/app/schedules")]

    public class ScheduleController : ScheduleControllerBase, ISchedulesAppService
    {
        public ScheduleController(ISchedulesAppService schedulesAppService) : base(schedulesAppService)
        {
        }
    }
}