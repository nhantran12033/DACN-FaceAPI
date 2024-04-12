using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using FaceAPI.Timesheets;

namespace FaceAPI.Controllers.Timesheets
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Timesheet")]
    [Route("api/app/timesheets")]

    public class TimesheetController : TimesheetControllerBase, ITimesheetsAppService
    {
        public TimesheetController(ITimesheetsAppService timesheetsAppService) : base(timesheetsAppService)
        {
        }
    }
}