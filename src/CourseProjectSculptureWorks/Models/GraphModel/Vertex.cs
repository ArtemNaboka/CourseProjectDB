using CourseProjectSculptureWorks.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.GraphModel
{
    public class Vertex
    {
        public Location Location { get; set; }
        public bool IsInTree { get; set; }

        public Vertex(Location location)
        {
            Location = location;
            IsInTree = false;
        }
    }
}
