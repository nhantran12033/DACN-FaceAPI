using FaceAPI.Departments;

using System;
using System.Collections.Generic;

namespace FaceAPI.Salaries
{
    public abstract class SalaryWithNavigationPropertiesBase
    {
        public Salary Salary { get; set; } = null!;

        

        public List<Department> Departments { get; set; } = null!;
        
    }
}