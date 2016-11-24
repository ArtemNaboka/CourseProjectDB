using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.ViewModels
{
    public class ExcursionsViewModel
    {
        [Required]
        [Display(Name = "Тематика")]
        public string Subjects { get; set; }

        [Display(Name = "Местоположения")]
        public int[] LocationsId { get; set; }
        public int ExcursionId { get; set; }

        [Required]
        [Display(Name = "Вид экскурсии")]
        public int ExcursionTypeId { get; set; }

        [Display(Name = "Дата экскурсии")]
        public DateTime DateOfExcursion { get; set; }

        [Display(Name = "Количество людей")]
        [Range(1, 1000)]
        public int NumberOfPeople { get; set; }

    }
}
