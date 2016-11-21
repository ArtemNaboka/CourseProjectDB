using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.Entities
{
    public class Composition
    {
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public int ExcursionId { get; set; }
        public virtual Excursion Excursion { get; set; }

        public int SerialNumber { get; set; }
    }
}
