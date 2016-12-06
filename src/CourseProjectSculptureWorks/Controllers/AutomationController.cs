using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseProjectSculptureWorks.Data;
using CourseProjectSculptureWorks.Models.Entities;
using CourseProjectSculptureWorks.Models.SalesmanModel;

namespace CourseProjectSculptureWorks.Controllers
{
    public class AutomationController : Controller
    {

        private readonly ApplicationDbContext _db;

        public AutomationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult CombinationsOfExcursions(string city, int minutesForExcursions,
                                                        int excursionTypeId, int numberOfpeople)
        {
            var locationsInCity = getCombination(_db.Locations
                                                .Where(l => l.City == city)
                                                .ToList())
                                                .OrderByDescending(l => l.Count)
                                                .ToList();
            var resultList = new List<List<Location>>();
            foreach(var locations in locationsInCity)
            {
                if(locations.Select(l => l.DurationOfExcursion).Sum() <= minutesForExcursions)
                {
                    resultList.Add(locations);
                }
            }

            /////////////////////////////////////////////
            var new_lists = new List<List<Location>>();
            foreach(var list in resultList)
            {
                var tempPerm = getPer(list);
                if (tempPerm.Locations.Select(l => l.DurationOfExcursion).Sum() + tempPerm.Duration <= minutesForExcursions)
                    new_lists.Add(list);
            }
            //////////////////////////////////////////////

            ViewBag.ExcursionType = _db.ExcursionTypes.Single(e => e.ExcursionTypeId == excursionTypeId);
            ViewBag.NumberOfPeople = numberOfpeople;
            ViewBag.Time = minutesForExcursions;
            return View(resultList);
        }



        [HttpPost]
        public IActionResult AddExcursions(int? numberOfPeople, int? typeOfExcursions, 
                                            DateTime? date, Location[] locations)
        {
            if (locations == null)
                throw new Exception("GG WP");
            return View();
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



        private void swap(Location a, Location b)
        {
            if (a == b) return;

            var tempLocation = a;
            a = b;
            b = tempLocation;
        }

        private ListDurationViewModel getPer(List<Location> list)
        {
            int x = list.Count - 1;
            var locationList = new List<List<Location>>();
            getPer(locationList, list, 0, x);
            return getMin(locationList);
        }


        private void getPer(List<List<Location>> locationsList, List<Location> list, int k, int m)
        {
            if (k == m)
            {
                Console.WriteLine(list);
            }
            else
                for (int i = k; i <= m; i++)
                {
                    swap(list[k], list[i]);
                    getPer(locationsList, list, k + 1, m);
                    swap(list[k], list[i]);
                }
        }


        private ListDurationViewModel getMin(List<List<Location>> lists)
        {
            int[] durations = new int[lists[0].Count];
            int count = 0;
            foreach(var list in lists)
            {
                var tempDuration = 0;
                for(int i = 1; i < list.Count; i++)
                {
                    var tempTransfer = _db.Transfers.SingleOrDefault(t => t.StartLocationId == list[i].LocationId
                                                                    && t.FinishLocationId == list[i - 1].LocationId);
                    if (tempTransfer != null)
                        tempDuration += tempTransfer.Duration;
                    else
                        throw new Exception("Не везде есть объединения");
                }
                durations[count++] = tempDuration;
            }

            var minIndex = Array.IndexOf(durations, durations.Min());
            return new ListDurationViewModel(lists[minIndex], durations.Min());
        }



        //private List<Location> getArranging(List<Location> list)
        //{
        //    List<List<Location>> prev, cur;
        //    Location el;
        //    int length = list.Count;

        //    cur = new List<List<Location>>()
        //    {
        //        new List<Location> { list[0] }
        //    };

        //    for(int i = 1; i < length; i++)
        //    {
        //        el = list[i];
        //        prev = cur;
        //        cur = new List<List<Location>>();

        //        prev.ForEach(l => cur = cur.Union(makeArr(l, el)).ToList());
        //    }

        //    return cur[0];
        //}


        //private List<List<Location>> makeArr(List<Location> list, Location el)
        //{
        //    return null;
        //}
    }
}
