using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.Entities
{
    public class Sculptor
    {
        [Key]
        public int SculptorId { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Country { get; set; }

        [Required]
        [Range(0, 1995)]
        public int YearOfBirth { get; set; }

        [Range(0, 2016)]
        public int? YearOfDeath { get; set; }

        public virtual List<Sculpture> Sculptures { get; set; }
    }
}
