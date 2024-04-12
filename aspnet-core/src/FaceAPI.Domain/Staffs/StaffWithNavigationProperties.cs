using FaceAPI.Departments;
using FaceAPI.Titles;
using FaceAPI.Timesheets;

using System;
using System.Collections.Generic;

namespace FaceAPI.Staffs
{
    public abstract class StaffWithNavigationPropertiesBase
    {
        public Staff Staff { get; set; } = null!;

        public Department Department { get; set; } = null!;
        public Title Title { get; set; } = null!;
        

        public List<Timesheet> Timesheets { get; set; } = null!;
        
    }
}