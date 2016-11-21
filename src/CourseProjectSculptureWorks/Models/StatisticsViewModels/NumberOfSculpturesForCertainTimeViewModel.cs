using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.StatisticsViewModels
{
    public class NumberOfSculpturesForCertainTimeViewModel
    {
        public string SculptureName { get; set; }
        public string SculptorName { get; set; }
        public string StyleName { get; set; }
        public string Country { get; set; }
        public int YearOfCreation { get; set; }
    }
}
