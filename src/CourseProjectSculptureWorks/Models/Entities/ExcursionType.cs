using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.Entities
{
    public class ExcursionType
    {
        [Key]
        public int ExcursionTypeId { get; set; }


        [Required]
        [Display(Name = "Имя вида")]
        public string NameOfType { get; set; }

        [Required]
        [Range(0, 100)]
        [Display(Name = "Скидка (%)")]
        public int Discount { get; set; }

        [Required]
        [Range(1, 200)]
        [Display(Name = "Минимальное кол-во человек")]
        public int MinNumberOfPeople { get; set; }

        [Required]
        [Range(1, 500)]
        [Display(Name = "Максимальное кол-во человек")]
        public int MaxNumberOfPeople { get; set; }


        public virtual List<Excursion> Excursions { get; set; }
    }
}
