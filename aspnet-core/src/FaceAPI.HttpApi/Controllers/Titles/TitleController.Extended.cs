using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using FaceAPI.Titles;

namespace FaceAPI.Controllers.Titles
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Title")]
    [Route("api/app/titles")]

    public class TitleController : TitleControllerBase, ITitlesAppService
    {
        public TitleController(ITitlesAppService titlesAppService) : base(titlesAppService)
        {
        }
    }
}