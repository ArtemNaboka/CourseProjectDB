using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseProjectSculptureWorks.Data;
using CourseProjectSculptureWorks.Models.Entities;
using CourseProjectSculptureWorks.Models.GraphModel;

namespace CourseProjectSculptureWorks.Controllers
{
    public class AutomationController : Controller
    {

        private readonly ApplicationDbContext _db;

        public AutomationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult CombinationsOfExcursions(string city, int minutesForExcursions)
        {
            //var locationsInCity = _db.Locations.Where(l => l.City == city).ToList();
            var locationsInCity = getCombination(_db.Locations
                                                .Where(l => l.City == city)
                                                .ToList());

            foreach(var locations in locationsInCity)
            {
                if (locations.Select(l => l.DurationOfExcursion).Sum() > minutesForExcursions)
                    continue;
                Graph tempGraph = new Graph(locations);
                for(int i = 0; i < locations.Count; i++)
                {
                    tempGraph.AddVertex(locations[i]);
                    for(var j = 0; j < i; j++)
                    {
                        ////if(_db.Transfers.SingleOrDefault(t => t.FirstLocation == locations[j]
                        ////&& t.SecondLocation == locations[i]) != null)
                        //{
                        //    tempGraph.AddEdge(i, j, _db.Transfers.SingleOrDefault(t => t.FirstLocation == locations[j]
                        ////&& t.SecondLocation == locations[i]));
                        //}
                    }
                }
            }
            return View(locationsInCity);
        }


        private List<List<Location>> getCombination(List<Location> list)
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
