using FaceAPI.Departments;

using System;
using System.Collections.Generic;

namespace FaceAPI.Positions
{
    public abstract class PositionWithNavigationPropertiesBase
    {
        public Position Position { get; set; } = null!;

        public Department Department { get; set; } = null!;
        

        
    }
}