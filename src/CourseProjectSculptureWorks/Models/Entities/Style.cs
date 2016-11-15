using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.Entities
{
    public class Style
    {
        [Key]
        public int StyleId { get; set; }

        [Display(Name = "Название стиля")]
        public string StyleName { get; set; }

        [Required]
        [Display(Name = "Эпоха возникновения")]
        public string Era { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "Страна возникновения")]
        public string Country { get; set; }

        public virtual List<Sculpture> Sculptures { get; set; }
    }
}
