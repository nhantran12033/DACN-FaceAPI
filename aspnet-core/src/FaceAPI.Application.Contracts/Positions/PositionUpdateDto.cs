using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.Positions
{
    public abstract class PositionUpdateDtoBase : IHasConcurrencyStamp
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Note { get; set; }
        public Guid? DepartmentId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}