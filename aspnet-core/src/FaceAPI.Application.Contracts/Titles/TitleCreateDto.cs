using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FaceAPI.Titles
{
    public abstract class TitleCreateDtoBase
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Note { get; set; }
    }
}