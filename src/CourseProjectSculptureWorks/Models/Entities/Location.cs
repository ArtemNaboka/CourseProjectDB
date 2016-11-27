using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.Entities
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }


        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Название")]
        public string LocationName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Вид")]
        public string LocationType { get; set; }

        [Required]
        [Range(10, 300)]
        [Display(Name = "Длительность экскурсии (мин)")]
        public int DurationOfExcursion { get; set; }

        [Required]
        [Range(0, 1000)]
        [Display(Name = "Цена экскурсии для человека (UAN)")]
        public decimal PriceForPerson { get; set; }

        public virtual List<Sculpture> Sculptures { get; set; }
        public virtual List<Composition> Compositions { get; set; }

        [InverseProperty("StartLocation")]
        public virtual List<Transfer> StartTransfers { get; set; }

        [InverseProperty("FinishLocation")]
        public virtual List<Transfer> FinishTransfers { get; set; }
    }
}
