using CourseProjectSculptureWorks.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.SalesmanModel
{
    public class Vertex
    {
        public Location Location { get; set; }
        public bool IsMarked { get; set; }

        public Vertex(Location location)
        {
            Location = location;
            IsMarked = false;
        }
    }
}
