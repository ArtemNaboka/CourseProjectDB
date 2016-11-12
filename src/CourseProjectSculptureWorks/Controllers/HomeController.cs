using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseProjectSculptureWorks.Data;
using CourseProjectSculptureWorks.Models.Entities;
using CourseProjectSculptureWorks.Models.SculptureViewModels;
using System.Text;

namespace CourseProjectSculptureWorks.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public IActionResult Styles(string searchString = null, int sortNum = 0, string[] boxFilter = null)
        {
            var searchedStyles = _db.Styles.ToList();
            if (searchString != null && searchString != String.Empty)
            {
                searchedStyles = searchedStyles.Where(s => s.StyleName.Trim().ToLower().Contains(searchString.Trim().ToLower())
                    || s.Country.Trim().ToLower().Contains(searchString.Trim().ToLower())).ToList();
                ViewBag.SearchString = searchString;
            }
            if (sortNum != 0)
            {
                switch (sortNum)
                {
                    case 1:
                        searchedStyles = searchedStyles.OrderBy(s => s.Era).ToList();
                        break;
                    case 2:
                        searchedStyles = searchedStyles.OrderBy(s => s.Country).ToList();
                        break;
                }
                ViewBag.Checked = sortNum;
            }
            if(boxFilter != null && boxFilter.Length != 0)
            {
                var temp = searchedStyles;
                List<int> filters = new List<int>();
                searchedStyles = searchedStyles.Where(s => boxFilter.Contains(s.Era)).ToList();
                foreach (var filter in boxFilter)
                {
                    //searchedStyles = searchedStyles.Where(s => s.Era == filter).ToList();
                    if (filter == "Античность")
                        filters.Add(1);
                    else if (filter == "Средневековье")
                        filters.Add(2);
                    else if (filter == "Ренесанс")
                        filters.Add(3);
                    else if (filter == "Новое время")
                        filters.Add(4);
                }
                ViewBag.Filters = filters;
            }
            return View(searchedStyles);
        }


        //[HttpGet]
        //public IActionResult Styles()
        //{
        //    return View(_db.Styles.ToList());
        //}


        //[HttpPost]
        //public IActionResult Styles(string searchString)
        //{
        //    if (searchString == null) searchString = String.Empty;
        //    var searchedStyles = _db.Styles.Where(s => s.StyleName.Trim().ToLower().Contains(searchString.Trim().ToLower())
        //        || s.Country.Trim().ToLower().Contains(searchString.Trim().ToLower())).ToList();
        //    ViewBag.SearchString = searchString;
        //    return View(searchedStyles);
        //}


        [HttpGet]
        public IActionResult EditStyle(int? styleId)
        {
            if (styleId == null) return NotFound();
            var style = _db.Styles.Single(s => s.Id == styleId);
            return View(style);
        }


        [HttpPost]
        public IActionResult EditStyle(Style style)
        {
            if (ModelState.IsValid)
            {
                _db.Styles.Update(style);
                _db.SaveChanges();
                return RedirectToAction(nameof(Styles));
            }
            return View(style);
        }


        [HttpGet]
        public IActionResult CreateNewStyle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNewStyle(Style style)
        {
            if (ModelState.IsValid)
            {
                _db.Styles.Add(style);                
                _db.SaveChanges();
                return RedirectToAction(nameof(Styles));
            }
            return View(style);
        }


        [HttpPost]
        public bool DeleteStyle(IntegerModel model)
        {
            var style = _db.Styles.Single(s => s.Id == model.Integer);
            _db.Styles.Remove(style);
            _db.SaveChanges();
            return _db.Styles != null && _db.Styles.Count() != 0;
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }



        public IActionResult ChangeStyleList(IntegerModel model)
        {
            if (model.Integer == 1)
                return RedirectToAction(nameof(Styles), new { styles = _db.Styles.OrderBy(s => s.Era) });
            else if(model.Integer == 2)
                return RedirectToAction(nameof(Styles), new { styles = _db.Styles.OrderBy(s => s.Country) });
            return null;
        }

        public IActionResult SearchStyles(string searchString)
        {
            var searchedStyles = _db.Styles.Where(s => s.StyleName.Trim().ToLower().Contains(searchString.Trim().ToLower())
                || s.Country.Trim().ToLower().Contains(searchString.Trim().ToLower())).ToList();
            return RedirectToAction(nameof(Styles), new { styles = searchedStyles });
        }
    }
}
