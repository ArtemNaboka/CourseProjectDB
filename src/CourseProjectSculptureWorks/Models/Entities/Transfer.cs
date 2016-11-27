using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.Entities
{
    public class Transfer
    {
        public int StartLocationId { get; set; }
        public int FinishLocationId { get; set; }
        public int Duration { get; set; }

        public virtual Location StartLocation { get; set; }
        public virtual Location FinishLocation { get; set; }
    }
}
