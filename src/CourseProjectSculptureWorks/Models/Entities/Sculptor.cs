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
        [Display(Name = "Имя скульптора")]
        public string Name { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Required]
        [Range(0, 1995)]
        [Display(Name = "Год рождения")]
        public int YearOfBirth { get; set; }

        [Range(0, 2016)]
        [Display(Name = "Год смерти (необязательно)")]
        public int? YearOfDeath { get; set; }

        public virtual List<Sculpture> Sculptures { get; set; }
    }
}
