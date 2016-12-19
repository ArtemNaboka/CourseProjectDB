using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseProjectSculptureWorks.Data;
using CourseProjectSculptureWorks.Models.ReportsViewModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CourseProjectSculptureWorks.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ReportController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public IActionResult WorksOfSculptorReport(int sculptorId)
        {
            //CreateDoc();
            var sculptor = _db.Sculptors.Single(s => s.SculptorId == sculptorId);
            var reportList = _db.Sculptures.Include(s => s.Style).Include(s => s.Sculptor)
                                .Where(s => s.Sculptor == sculptor)
                                .ToList();
            var groupedList = reportList.GroupBy(s => s.Style.StyleName)
                                    .Select(g => new WorksOfSculptorViewModel
                                    {
                                        StyleName = g.Key,
                                        NumberOfSculptures = reportList.Where(r => r.Style.StyleName == g.Key).Count()
                                    })
                                    .ToList();
            ViewData["SculptorName"] = sculptor.Name;
            ViewData["NumberOfSculptures"] = reportList.Count; 
            return View(groupedList);
        }


        [HttpGet]
        public IActionResult ConductedExcursionsReport()
        {
            var groupedByLocation = _db.Compositions
                                        .Include(c => c.Excursion)
                                        .ThenInclude(e => e.ExcursionType)
                                        .Include(c => c.Location)
                                        .Where(c => c.Excursion.DateOfExcursion < DateTime.Now)
                                        .Select(c => new ConductedExcursionViewModel
                                        {
                                             LocationName = c.Location.LocationName,
                                             NameOfExcursionType = c.Excursion.ExcursionType.NameOfType
                                        })
                                        .ToList()
                                        .GroupBy(c => c.LocationName);

            List<ConductedExcursionViewModel> resultList = new List<ConductedExcursionViewModel>();
            foreach(var item in groupedByLocation)
            {
                var groupedByExcursionTypeList = item.GroupBy(i => i.NameOfExcursionType)
                                                        .Select(i => new ConductedExcursionViewModel
                                                        {
                                                            LocationName = item.Key,
                                                            NameOfExcursionType = i.Key,
                                                            ExcursionsNumber = i.Count()
                                                        });
                foreach (var element in groupedByExcursionTypeList)
                    resultList.Add(element);
            }
            ViewData["NumberOfExcursions"] = _db.Excursions.Count();
            return View(resultList);
        }
        

        [HttpGet]
        public IActionResult ConductedExcursions(int? locationId)
        {
            if (locationId == null)
                return NotFound();

            var location = _db.Locations.Single(l => l.LocationId == locationId);

            var excursionsId = _db.Compositions
                                        .Include(c => c.Excursion)
                                        .ThenInclude(e => e.ExcursionType)
                                        .Include(c => c.Location)
                                        .Where(c => c.LocationId == locationId)
                                        .Select(c => c.ExcursionId)
                                        .ToList();

            var excursions = _db.Excursions
                .Include(e => e.ExcursionType)
                .Where(e => excursionsId.Contains(e.ExcursionId))
                .OrderBy(e => e.DateOfExcursion)
                .ToList();

            ViewData["Location"] = location.LocationName;
            return View(excursions);
        }
    }
}
