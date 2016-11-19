using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models
{
    public static class Lists
    {
        public static List<string> SculptureTypes = new List<string>()
        {
            "Круглая",
            "Рельефная"
        };

        public static List<string> Era = new List<string>()
        {
            "Античность",
            "Средневековье",
            "Ренесанс",
            "Новое время"
        };

        public static List<string> LocationTypes = new List<string>()
        {
            "Музей",
            "Парк",
            "Сквер",
            "Выставка"
        };

    }
}
