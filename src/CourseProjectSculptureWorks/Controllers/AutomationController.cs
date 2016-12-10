using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseProjectSculptureWorks.Data;
using CourseProjectSculptureWorks.Models.Entities;
using CourseProjectSculptureWorks.Models.SalesmanModel;
using CourseProjectSculptureWorks.Models.ViewModels;

namespace CourseProjectSculptureWorks.Controllers
{
    public class AutomationController : Controller
    {

        private readonly ApplicationDbContext _db;

        public AutomationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult CombinationsOfExcursions(string subjects, DateTime? date, string city, int minutesForExcursions,
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
            var new_lists = new List<ListDurationViewModel>();
            foreach (var list in resultList)
            {
                var tempPerm = getMin(getPermutations(list, list.Count));
                if (tempPerm.Locations.Select(l => l.DurationOfExcursion).Sum() + tempPerm.Duration <= minutesForExcursions)
                    new_lists.Add(tempPerm);
            }
            //////////////////////////////////////////////

            ViewBag.ExcursionType = _db.ExcursionTypes.Single(e => e.ExcursionTypeId == excursionTypeId);
            ViewBag.NumberOfPeople = numberOfpeople;
            ViewBag.Time = minutesForExcursions;
            ViewBag.City = city;
            ViewBag.Subjects = subjects;
            ViewBag.Date = date; 

            return View(new_lists);
        }

        [HttpPost]
        public IActionResult AddExcursions(string subjects, int? numberOfPeople, int? typeOfExcursions, 
                                            string date, [FromForm]int[] locationsId)
        {
            ExcursionsViewModel model = new ExcursionsViewModel
            {
                DateOfExcursion = Convert.ToDateTime(date),
                ExcursionTypeId = typeOfExcursions.Value,
                LocationsId = locationsId,
                NumberOfPeople = numberOfPeople.Value,
                Subjects = subjects
            };

            var locations = new List<Location>();
            foreach (var location_id in locationsId)
                locations.Add(_db.Locations
                    .Single(l => l.LocationId == location_id));
            var excursionType = _db.ExcursionTypes
                .Single(e => e.ExcursionTypeId == model.ExcursionTypeId);
            var excursion = new Excursion
            {
                Subjects = model.Subjects,
                DateOfExcursion = model.DateOfExcursion,
                ExcursionType = excursionType,
                NumberOfPeople = model.NumberOfPeople,
                PriceOfExcursion = locations.Select(l => l.PriceForPerson)
                    .Sum()
                    * model.NumberOfPeople *
                    (1 - excursionType.Discount / 100)
            };
            _db.Excursions.Add(excursion);
            _db.SaveChanges();

            foreach (var location_id in locationsId)
            {
                _db.Compositions.Add(new Composition
                {
                    ExcursionId = excursion.ExcursionId,
                    LocationId = location_id,
                    Excursion = excursion,
                    Location = _db.Locations
                        .Single(l => l.LocationId == location_id)
                });
            }
            _db.SaveChanges();
            return RedirectToAction("Excursions", "Home");
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



        private List<List<Location>> getPermutations(List<Location> list, int length)
        {
            if(length == 1)
                return list.Select(t => new List<Location>() { t }).ToList();

            return getPermutations(list, length - 1)
                    .SelectMany(t => list.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new List<Location> { t2 }).ToList())
                    .ToList();
        }



        private ListDurationViewModel getMin(List<List<Location>> lists)
        {
            int[] durations = new int[lists.Count];
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


    }
}
