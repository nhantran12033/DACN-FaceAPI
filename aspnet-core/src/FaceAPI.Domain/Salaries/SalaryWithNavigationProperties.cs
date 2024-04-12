using FaceAPI.Departments;
using FaceAPI.Titles;

using System;
using System.Collections.Generic;

namespace FaceAPI.Salaries
{
    public abstract class SalaryWithNavigationPropertiesBase
    {
        public Salary Salary { get; set; } = null!;

        public Department Department { get; set; } = null!;
        public Title Title { get; set; } = null!;
        

        
    }
}