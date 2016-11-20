using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseProjectSculptureWorks.Data;
using CourseProjectSculptureWorks.Models.Entities;
using CourseProjectSculptureWorks.Models.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

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


        #region SculptureController
        [HttpGet]
        public async Task<IActionResult> Sculptures(string searchString = null, int sortNum = 0, string[] boxFilter = null)
        {
            var searchedSculptures = await _db.Sculptures.Include(s => s.Sculptor)
                .Include(s => s.Style)
                .Include(s => s.Location)
                .ToListAsync();
            if (searchString != null && searchString != String.Empty)
            {
                searchedSculptures = searchedSculptures.Where(s => s.Name.Trim().ToLower().Contains(searchString.Trim().ToLower())
                    || s.Sculptor.Name.Trim().ToLower().Contains(searchString.Trim().ToLower())
                    || s.Style.StyleName.Trim().ToLower().Contains(searchString.Trim().ToLower())
                    || s.Location.LocationName.Trim().ToLower().Contains(searchString.Trim().ToLower())
                    || s.Material.Trim().ToLower().Contains(searchString.Trim().ToLower())
                    || s.Year.ToString().Trim().ToLower().Contains(searchString.Trim().ToLower())
                    || s.Square.ToString().Trim().ToLower().Contains(searchString.Trim().ToLower())
                    || s.Height.ToString().Trim().ToLower().Contains(searchString.Trim().ToLower())).ToList();
                ViewBag.SearchString = searchString;
            }
            if (sortNum != 0)
            {
                switch (sortNum)
                {
                    case 1:
                        searchedSculptures = searchedSculptures.OrderBy(s => s.Type).ToList();
                        break;
                    case 2:
                        searchedSculptures = searchedSculptures.OrderBy(s => s.Sculptor.Name).ToList();
                        break;
                    case 3:
                        searchedSculptures = searchedSculptures.OrderBy(s => s.Style.StyleName).ToList();
                        break;
                    case 4:
                        searchedSculptures = searchedSculptures.OrderBy(s => s.Location.LocationName).ToList();
                        break;
                    case 5:
                        searchedSculptures = searchedSculptures.OrderBy(s => s.Material).ToList();
                        break;
                    case 6:
                        searchedSculptures = searchedSculptures.OrderBy(s => s.Year).ToList();
                        break;
                    case 7:
                        searchedSculptures = searchedSculptures.OrderBy(s => s.Square).ToList();
                        break;
                    case 8:
                        searchedSculptures = searchedSculptures.OrderBy(s => s.Height).ToList();
                        break;
                }
                ViewBag.Checked = sortNum;
            }
            if (boxFilter != null && boxFilter.Length != 0)
            {
                var temp = searchedSculptures;
                List<int> filters = new List<int>();
                searchedSculptures = searchedSculptures.Where(s => boxFilter.Contains(s.Type)).ToList();
                foreach (var filter in boxFilter)
                {
                    if (filter == "Круглая")
                        filters.Add(1);
                    else if (filter == "Рельефная")
                        filters.Add(2);
                }
                ViewBag.Filters = filters;
            }
            return View(searchedSculptures);
        }


        [HttpGet]
        public IActionResult AddNewSculpture()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddNewSculpture(SculptureViewModel model)
        {
            if(ModelState.IsValid)
            {
                var sculpture = new Sculpture
                {
                    Name = model.Name,
                    Type = model.Type,
                    Material = model.Material,
                    Year = model.Year,
                    Square = model.Square,
                    Height = model.Height,
                    Style = await _db.Styles.SingleAsync(s => s.StyleId == model.StyleId),
                    Sculptor = await _db.Sculptors.SingleAsync(s => s.SculptorId == model.SculptorId),
                    Location = await _db.Locations.SingleAsync(l => l.LocationId == model.LocationId)
                };
                _db.Sculptures.Add(sculpture);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Sculptures));
            }
            return View(model);
        }


        public async Task<IActionResult> EditSculpture(int? sculptureId)
        {
            if (sculptureId == null)
                return NotFound();
            var sculpture = await _db.Sculptures
                .Include(s => s.Sculptor)
                .Include(s => s.Style)
                .Include(s => s.Location).SingleAsync(s => s.Id == sculptureId.Value);
            var sculptureViewModel = new SculptureViewModel
            {
                SculptureId = sculpture.Id,
                Name = sculpture.Name,
                Type = sculpture.Type,
                Material = sculpture.Material,
                Year = sculpture.Year,
                Square = sculpture.Square,
                Height = sculpture.Height,
                StyleId = sculpture.Style.StyleId,
                SculptorId = sculpture.Sculptor.SculptorId,
                LocationId = sculpture.Location.LocationId
            };
            return View(sculptureViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> EditSculpture(SculptureViewModel model)
        {
            if(ModelState.IsValid)
            {
                var sculpture = new Sculpture
                {
                    Id = model.SculptureId,
                    Name = model.Name,
                    Type = model.Type,
                    Material = model.Material,
                    Year = model.Year,
                    Square = model.Square,
                    Height = model.Height,
                    Style = await _db.Styles.SingleAsync(s => s.StyleId == model.StyleId),
                    Sculptor = await _db.Sculptors.SingleAsync(s => s.SculptorId == model.SculptorId),
                    Location = await _db.Locations.SingleAsync(l => l.LocationId == model.LocationId)
                };
                _db.Sculptures.Update(sculpture);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Sculptures));
            }
            return View(model);
        }


        [HttpPost]
        public async Task<bool> DeleteSculpture(IntegerModel model)
        {
            var sculpture = _db.Sculptures.Single(s => s.Id == model.Integer);
            _db.Sculptures.Remove(sculpture);
            await _db.SaveChangesAsync();
            return _db.Sculptures != null && _db.Sculptures.Count() != 0;
        }

        #endregion

        #region SculptorController
        public IActionResult Sculptors(int searchCriteria = 0, string searchString = null, int sortNum = 0)
        {
            var searchedSculptors = _db.Sculptors.ToList();
            if (searchString != null && searchString != String.Empty)
            {
                int year;
                if (searchCriteria == 3 || searchCriteria == 4)
                {
                    if (int.TryParse(searchString, out year))
                    {
                        if (searchCriteria == 3)
                            searchedSculptors = _db.Sculptors.Where(s => s.YearOfBirth == year).ToList();
                        else if (searchCriteria == 4)
                            searchedSculptors = _db.Sculptors.Where(s => s.YearOfDeath == year).ToList();
                    }
                    else
                        searchedSculptors = new List<Sculptor>();      
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

        #region LocationController
        public IActionResult Locations(string searchString, int sortNum, string[] boxFilter)
        {
            var searchedLocation = _db.Locations.ToList();
            if (searchString != null && searchString != String.Empty)
            {
                searchedLocation = searchedLocation.Where(s => s.LocationName.Trim().ToLower().Contains(searchString.Trim().ToLower())
                    || s.Country.Trim().ToLower().Contains(searchString.Trim().ToLower())
                    || s.City.Trim().ToLower().Contains(searchString.Trim().ToLower())
                    || s.Address.Trim().ToLower().Contains(searchString.Trim().ToLower())
                    || s.DurationOfExcursion.ToString().Trim().ToLower().Contains(searchString.Trim().ToLower())
                    || s.PriceForPerson.ToString().Trim().ToLower().Contains(searchString.Trim().ToLower())).ToList();
                ViewBag.SearchString = searchString;
            }
            if (sortNum != 0)
            {
                switch (sortNum)
                {
                    case 1:
                        searchedLocation = searchedLocation.OrderBy(s => s.LocationType).ToList();
                        break;
                    case 2:
                        searchedLocation = searchedLocation.OrderBy(s => s.Country).ToList();
                        break;
                    case 3:
                        searchedLocation = searchedLocation.OrderBy(s => s.City).ToList();
                        break;
                    case 4:
                        searchedLocation = searchedLocation.OrderBy(s => s.PriceForPerson).ToList();
                        break;
                    case 5:
                        searchedLocation = searchedLocation.OrderBy(s => s.DurationOfExcursion).ToList();
                        break;
                }
                ViewBag.Checked = sortNum;
            }
            if (boxFilter != null && boxFilter.Length != 0)
            {
                var temp = searchedLocation;
                List<int> filters = new List<int>();
                searchedLocation = searchedLocation.Where(s => boxFilter.Contains(s.LocationType)).ToList();
                foreach (var filter in boxFilter)
                {
                    if (filter == "Музей")
                        filters.Add(1);
                    else if (filter == "Парк")
                        filters.Add(2);
                    else if (filter == "Сквер")
                        filters.Add(3);
                    else if (filter == "Выставка")
                        filters.Add(4);
                }
                ViewBag.Filters = filters;
            }
            return View(searchedLocation);
        }


        [HttpGet]
        public IActionResult AddNewLocation()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddNewLocation(Location location)
        {
            if(ModelState.IsValid)
            {
                _db.Locations.Add(location);
                _db.SaveChanges();
                return RedirectToAction(nameof(Locations));
            }
            return View(location);
        }


        [HttpGet]
        public IActionResult EditLocation(int? locationId)
        {
            if (locationId == null)
                return NotFound();
            var location = _db.Locations.Single(l => l.LocationId == locationId);
            return View(location);
        }


        [HttpPost]
        public IActionResult EditLocation(Location location)
        {
            if(ModelState.IsValid)
            {
                _db.Locations.Update(location);
                _db.SaveChanges();
                return RedirectToAction(nameof(Locations));
            }
            return View(location);
        }


        [HttpPost]
        public bool DeleteLocation(IntegerModel model)
        {
            var location = _db.Locations.Single(l => l.LocationId == model.Integer);
            _db.Locations.Remove(location);
            _db.SaveChanges();
            return _db.Locations != null && _db.Locations.Count() != 0;
        }

        #endregion

        #region ExcursionTypesController

        // Добавить фильтр, сорт и поиск
        public async Task<IActionResult> ExcursionTypes()
        {
            return View(await _db.ExcursionTypes.ToListAsync());
        }

        [HttpGet]
        public IActionResult AddNewExcursionType()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddNewExcursionType(ExcursionType excursionType)
        {
            if(ModelState.IsValid)
            {
                _db.ExcursionTypes.Add(excursionType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(ExcursionTypes));
            }
            return View(excursionType);
        }

        [HttpGet]
        public async Task<IActionResult> EditExcursionType(int? excursionTypeId)
        {
            if (excursionTypeId == null)
                return NotFound();
            var excursionType = await _db.ExcursionTypes.SingleAsync(et => et.ExcursionTypeId == excursionTypeId);
            return View(excursionType);
        }


        [HttpPost]
        public async Task<IActionResult> EditExcursionType(ExcursionType excursionType)
        {
            if(ModelState.IsValid)
            {
                _db.ExcursionTypes.Update(excursionType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(ExcursionTypes));
            }
            return View();
        }


        [HttpPost]
        public async Task<bool> DeleteExcursionType(IntegerModel model)
        {
            var excursionType = _db.ExcursionTypes.Single(s => s.ExcursionTypeId == model.Integer);
            _db.ExcursionTypes.Remove(excursionType);
            await _db.SaveChangesAsync();
            return _db.ExcursionTypes != null && _db.ExcursionTypes.Count() != 0;
        }

        #endregion

        #region Statistics

        //public IActionResult NumberOfSculpturesForCertainTime()
        //{
        //    var year = 1000;
        //    int yearOfStart = DateTime.Now.Year - year;
        //    var styles = _db.Sculptures.Include(s => s.Sculptor)
        //        .Include(s => s.Style)
        //        .Include(s => s.Location)
        //        .ToListAsync();
        //    var union = _db.Sculptures.Join(_db.Sculptors, s => s.Sculptor.SculptorId, s => s.SculptorId, new { });
        //}

        #endregion

        #region Other
        public IActionResult About()
        {
            ViewData["Message"] = "Набока Артем ПИ-15-1";

            return View();
        }

        public FileResult GetDoc([FromServices] IHostingEnvironment appEnvironment)
        {
            string file_path = Path.Combine(appEnvironment.ContentRootPath, "Docs/Набока Артем - Записка.docx");
            string file_type = "application/docx";
            string file_name = "Набока Артем - Записка.docx";
            return PhysicalFile(file_path, file_type, file_name);
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
