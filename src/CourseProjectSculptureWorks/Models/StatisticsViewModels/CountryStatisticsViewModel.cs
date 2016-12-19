using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.StatisticsViewModels
{
    public class CountryStatisticsViewModel
    {
        public string Country { get; set; }
        public int NumberOfLocations { get; set; }
        public int NumberOfSculptures { get; set; }
        public int NumberOfSculptors { get; set; }
    }
}
