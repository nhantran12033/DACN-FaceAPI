using Volo.Abp.Application.Dtos;
using System;

namespace FaceAPI.Titles
{
    public abstract class GetTitlesInputBase : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Note { get; set; }

        public GetTitlesInputBase()
        {

        }
    }
}