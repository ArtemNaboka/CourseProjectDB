using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseProjectSculptureWorks.Data;
using Microsoft.EntityFrameworkCore;
using CourseProjectSculptureWorks.Models.StatisticsViewModels;

namespace CourseProjectSculptureWorks.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StatisticsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> NumberOfSculpturesForCertainTime(int? year)
        {
            if(year == null)
                return NotFound();

            var yearOfStart = DateTime.Now.Year - year.Value;

            ViewBag.Year = year.Value;
            var sculptures = await _db.Sculptures.Include(s => s.Sculptor)
                .Include(s => s.Style)
                .Include(s => s.Location)
                .ToListAsync();

            var statisticList = sculptures.Where(s => s.Year >= yearOfStart)
                                    .Select(s => new NumberOfSculpturesForCertainTimeViewModel
                                    {
                                        SculptureName = s.Name,
                                        SculptorName = s.Sculptor.Name,
                                        StyleName = s.Style.StyleName,
                                        Country = s.Location.Country,
                                        YearOfCreation = s.Year
                                    }).OrderBy(n => n.YearOfCreation).ToList();

            return View(statisticList);
        }


        [HttpGet]
        public async Task<IActionResult> StylesPopularity()
        {
            var styles = await _db.Styles.Include(s => s.Sculptures).ToListAsync();

            var statisticList = styles.Select(s => new StylePopularityViewModel
                                        {
                                            StyleName = s.StyleName,
                                            NumberOfSculpture = s.Sculptures.Count
                                        })
                                       .GroupBy(s => s.StyleName).ToList();

            return View(statisticList);                                    
        }


        [HttpGet]
        public async Task<IActionResult> AttendanceOfLocations()
        {

            var query = _db.Locations.GroupBy(l => l.City);
            var attendanceModelsList = new List<AttedanceViewModel>();

            foreach (var q in query)
            {
                var tempModel = new AttedanceViewModel() { City = q.Key };
                tempModel.NumberOfLocations = _db.Locations.Count(l => l.City == tempModel.City);

                var locationsInCityIds = await _db.Locations.Where(l => l.City == tempModel.City)
                                                            .Select(l => l.LocationId)
                                                            .ToListAsync();

                tempModel.NumberOfExcursions = _db.Compositions.Count(c => locationsInCityIds.Contains(c.LocationId)
                                                                    && _db.Excursions.Single(
                                                                    e => e.ExcursionId == c.ExcursionId)
                                                                    .DateOfExcursion < DateTime.Now);

                tempModel.NumberOfPeople = _db.Excursions.Where(e => e.DateOfExcursion < DateTime.Now
                                                                    && _db.Compositions
                                                                .Where(c => locationsInCityIds.Contains(c.LocationId))
                                                                .Select(c => c.ExcursionId)
                                                                .Contains(e.ExcursionId))
                                                                .Select(v => v.NumberOfPeople)
                                                                .Sum();

                attendanceModelsList.Add(tempModel);
            }

            return View(attendanceModelsList);
        }


        public async Task<IActionResult> CountryStatistics()
        {
            var countries = await _db.Locations
                .Select(l => l.Country)
                .Distinct()
                .ToListAsync();


            var listToView = new List<CountryStatisticsViewModel>();

            foreach (var country in countries)
            {
                var locationsOfCountry = _db.Locations
                    .Where(l => l.Country == country)
                    .Select(l => l.LocationId)
                    .ToList();


                listToView.Add(new CountryStatisticsViewModel()
                {
                    Country = country,
                    NumberOfLocations = _db.Locations.Count(l => l.Country == country),

                    NumberOfSculptures = _db.Sculptures
                        .Include(s => s.Location)
                        .ToList()
                        .Count(s => locationsOfCountry.Contains(s.Location.LocationId)),

                    NumberOfSculptors = _db.Sculptors.Count(s => s.Country == country)
                });              
            }

            return View(listToView);
        }
    }
}
