using Volo.Abp.Application.Dtos;
using System;

namespace FaceAPI.Positions
{
    public abstract class GetPositionsInputBase : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Note { get; set; }
        public Guid? DepartmentId { get; set; }

        public GetPositionsInputBase()
        {

        }
    }
}