using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.Entities
{
    public class Excursion
    {
        [Key]
        public int ExcursionId { get; set; }

        [Required]
        public DateTime DateOfExcursion { get; set; }

        public int NumberOfPeople { get; set; }
        public decimal PriceOfExcursion { get; set; }
        public string Subjects { get; set; }

        public virtual ExcursionType ExcursionType { get; set; }
        public virtual List<Composition> Compositions { get; set; }
    }
}
