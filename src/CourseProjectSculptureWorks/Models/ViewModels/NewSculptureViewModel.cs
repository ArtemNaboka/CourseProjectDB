using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.ViewModels
{
    public class NewSculptureViewModel
    {
        public int SculptureId { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Вид")]
        public string Type { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "Материал")]
        public string Material { get; set; }

        [Required]
        [Range(0, 2016)]
        [Display(Name = "Год создания")]
        public int Year { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Площадь")]
        public double Square { get; set; }

        [Required]
        [Range(0.2, 100)]
        [Display(Name = "Высота")]
        public double Height { get; set; }

        [Required]
        [Display(Name = "Стиль")]
        public string StyleName { get; set; }

        [Required]
        [Display(Name = "Скульптор")]
        public string SculptorName { get; set; }

        [Required]
        [Display(Name = "Местоположение")]
        public string LocationName { get; set; }
    }
}
