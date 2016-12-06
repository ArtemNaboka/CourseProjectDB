using CourseProjectSculptureWorks.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.SalesmanModel
{
    public class ListDurationViewModel
    {
        public List<Location> Locations { get; set; }
        public int Duration { get; set; }

        public ListDurationViewModel(List<Location> locations, int duration)
        {
            Locations = locations;
            Duration = duration;
        }
    }
}
