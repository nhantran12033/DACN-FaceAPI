using Volo.Abp.Application.Dtos;
using System;

namespace FaceAPI.Departments
{
    public abstract class GetDepartmentsInputBase : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Code { get; set; }
        public string? Name { get; set; }
        public DateTime? DateMin { get; set; }
        public DateTime? DateMax { get; set; }
        public string? Note { get; set; }
        public Guid? TitleId { get; set; }

        public GetDepartmentsInputBase()
        {

        }
    }
}