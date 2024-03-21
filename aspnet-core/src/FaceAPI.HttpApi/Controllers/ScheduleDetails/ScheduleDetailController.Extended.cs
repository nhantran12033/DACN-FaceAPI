using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using FaceAPI.ScheduleDetails;

namespace FaceAPI.Controllers.ScheduleDetails
{
    [RemoteService]
    [Area("app")]
    [ControllerName("ScheduleDetail")]
    [Route("api/app/schedule-details")]

    public class ScheduleDetailController : ScheduleDetailControllerBase, IScheduleDetailsAppService
    {
        public ScheduleDetailController(IScheduleDetailsAppService scheduleDetailsAppService) : base(scheduleDetailsAppService)
        {
        }
    }
}