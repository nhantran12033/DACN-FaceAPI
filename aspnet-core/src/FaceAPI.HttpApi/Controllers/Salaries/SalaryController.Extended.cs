using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using FaceAPI.Salaries;

namespace FaceAPI.Controllers.Salaries
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Salary")]
    [Route("api/app/salaries")]

    public class SalaryController : SalaryControllerBase, ISalariesAppService
    {
        public SalaryController(ISalariesAppService salariesAppService) : base(salariesAppService)
        {
        }
    }
}