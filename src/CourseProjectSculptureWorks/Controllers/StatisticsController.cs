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
        public async Task<IActionResult> NumberOfSculpturesForCertainTime(int year = 1000)
        {
            int yearOfStart = DateTime.Now.Year - year;
            ViewBag.Year = year;
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
                                    }).ToList();
            return View(statisticList);
        }
    }
}
