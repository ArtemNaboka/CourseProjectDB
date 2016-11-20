using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.Entities
{
    public class Sculpture
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Material { get; set; }

        [Required]
        [Range(0, 2016)]
        public int Year { get; set; }

        [Required]
        [Range(1, 100)]
        public double Square { get; set; }

        [Required]
        [Range(0.2, 1000)]
        public double Height { get; set; }

        public virtual Style Style { get; set; }
        public virtual Sculptor Sculptor { get; set; }
        public virtual Location Location { get; set; }
    }
}
