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

        #region SculptorController
        public IActionResult Sculptors(int searchCriteria = 0, string searchString = null, int sortNum = 0)
        {
            var searchedSculptors = _db.Sculptors.ToList();
            if (searchString != null && searchString != String.Empty)
            {
                int year;
                if (int.TryParse(searchString, out year))
                {
                    if (searchCriteria == 3)
                        searchedSculptors = _db.Sculptors.Where(s => s.YearOfBirth == year).ToList();
                    else if (searchCriteria == 4)
                        searchedSculptors = _db.Sculptors.Where(s => s.YearOfDeath == year).ToList();      
                }
                else
                {
                    if (searchCriteria == 1)
                        searchedSculptors = _db.Sculptors.Where(s => s.Name.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
                    else if(searchCriteria == 2)
                        searchedSculptors = _db.Sculptors.Where(s => s.Country.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
                }
                ViewBag.SearchString = searchString;
                ViewBag.SearchCriteria = searchCriteria;
            }
            //if(searchString != null && searchString != String.Empty)
            //{
            //    int year;
            //    if (int.TryParse(searchString, out year))
            //        searchedSculptors = _db.Sculptors.Where(s => s.YearOfBirth == year || s.YearOfDeath == year).ToList();
            //    else
            //        searchedSculptors = _db.Sculptors.Where(s => s.Name.ToLower().Trim().Contains(searchString.ToLower().Trim()) 
            //                        || s.Country.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            //    ViewBag.SearchString = searchString;
            //}
            if (sortNum != 0)
            {
                switch (sortNum)
                {
                    case 1:
                        searchedSculptors = searchedSculptors.OrderBy(s => s.Country).ToList();
                        break;
                    case 2:
                        searchedSculptors = searchedSculptors.OrderBy(s => s.YearOfBirth).ToList();
                        break;
                }
                ViewBag.Checked = sortNum;
            }
            return View(searchedSculptors);
        }


        [HttpGet]
        public IActionResult AddNewSculptor()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddNewSculptor(Sculptor sculptor)
        {
            if (sculptor.YearOfBirth > sculptor.YearOfDeath)
                ModelState.AddModelError("Dates", "Год смерти скульптора должен быть меньше года рождения");
            if (ModelState.IsValid)
            {
                _db.Sculptors.Add(sculptor);
                _db.SaveChanges();
                return RedirectToAction(nameof(Sculptors));
            }
            return View(sculptor);
        }

        [HttpGet]
        public IActionResult EditSculptor(int? sculptorId)
        {
            if (sculptorId == null) return NotFound();
            var sculptor = _db.Sculptors.Single(s => s.SculptorId == sculptorId);
            return View(sculptor);
        } 

        [HttpPost]
        public IActionResult EditSculptor(Sculptor sculptor)
        {
            if(sculptor.YearOfBirth > sculptor.YearOfDeath)
                ModelState.AddModelError("Dates", "Год смерти скульптора должен быть меньше года рождения");
            if(ModelState.IsValid)
            {
                _db.Update(sculptor);
                _db.SaveChanges();
                return RedirectToAction(nameof(Sculptors));
            }
            return View(sculptor);
        }

        [HttpPost]
        public bool DeleteSculptor(IntegerModel model)
        {
            var sculptor = _db.Sculptors.Single(s => s.SculptorId == model.Integer);
            _db.Sculptors.Remove(sculptor);
            _db.SaveChanges();
            return _db.Sculptors != null && _db.Sculptors.Count() != 0;
        }
        #endregion

        #region StyleController
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


        [HttpGet]
        public IActionResult EditStyle(int? styleId)
        {
            if (styleId == null) return NotFound();
            var style = _db.Styles.Single(s => s.StyleId == styleId);
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
            var style = _db.Styles.Single(s => s.StyleId == model.Integer);
            _db.Styles.Remove(style);
            _db.SaveChanges();
            return _db.Styles != null && _db.Styles.Count() != 0;
        }
        #endregion

        #region Shlak
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
        #endregion
    }
}
