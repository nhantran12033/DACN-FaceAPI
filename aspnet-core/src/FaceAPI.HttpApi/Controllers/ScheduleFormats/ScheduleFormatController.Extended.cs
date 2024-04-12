using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using FaceAPI.ScheduleFormats;

namespace FaceAPI.Controllers.ScheduleFormats
{
    [RemoteService]
    [Area("app")]
    [ControllerName("ScheduleFormat")]
    [Route("api/app/schedule-formats")]

    public class ScheduleFormatController : ScheduleFormatControllerBase, IScheduleFormatsAppService
    {
        public ScheduleFormatController(IScheduleFormatsAppService scheduleFormatsAppService) : base(scheduleFormatsAppService)
        {
        }
    }
}