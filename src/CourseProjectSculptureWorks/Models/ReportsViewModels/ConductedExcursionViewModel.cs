using CourseProjectSculptureWorks.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.ReportsViewModels
{
    public class ConductedExcursionViewModel
    {
        public string LocationName { get; set; }
        public string NameOfExcursionType { get; set; }
        public int ExcursionsNumber { get; set; }
    }
}
