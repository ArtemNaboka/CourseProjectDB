using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseProjectSculptureWorks.Data;
using CourseProjectSculptureWorks.Models.Entities;

namespace CourseProjectSculptureWorks.Controllers
{
    public class AutomationController : Controller
    {

        private readonly ApplicationDbContext _db;

        public AutomationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult CombinationsOfExcursions(string city)
        {
            var locationsInCity = _db.Locations.Where(l => l.City == city).ToList();
            return View(GetCombination(locationsInCity).OrderByDescending(l => l.Count).ToList());
        }


        private List<List<Location>> GetCombination(List<Location> list)
        {
            var resultList = new List<List<Location>>();
            double count = Math.Pow(2, list.Count);
            for (int i = 1; i <= count - 1; i++)
            {
                string str = Convert.ToString(i, 2).PadLeft(list.Count, '0');
                List<Location> tempList = new List<Location>();
                for (int j = 0; j < str.Length; j++)
                {
                    if (str[j] == '1')
                    {
                        tempList.Add(list[j]);
                    }
                }
                resultList.Add(tempList);
            }
            return resultList;
        }
    }
}
