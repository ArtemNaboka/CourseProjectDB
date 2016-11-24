using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseProjectSculptureWorks.Data;
using CourseProjectSculptureWorks.Models.ReportsViewModels;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult WorksOfSculptorReport(string sculptorName)
        {
            var sculptor = _db.Sculptors.Single(s => s.Name == sculptorName);
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
            ViewData["SculptorName"] = sculptorName;
            ViewData["NumberOfSculptures"] = reportList.Count; 
            return View(groupedList);
        }
    }
}
