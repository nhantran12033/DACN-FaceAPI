using FaceAPI.Titles;

using System;
using System.Collections.Generic;

namespace FaceAPI.Departments
{
    public abstract class DepartmentWithNavigationPropertiesBase
    {
        public Department Department { get; set; } = null!;

        

        public List<Title> Titles { get; set; } = null!;
        
    }
}