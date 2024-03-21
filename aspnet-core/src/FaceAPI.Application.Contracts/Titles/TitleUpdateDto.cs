using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace FaceAPI.Titles
{
    public abstract class TitleUpdateDtoBase : IHasConcurrencyStamp
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Note { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}