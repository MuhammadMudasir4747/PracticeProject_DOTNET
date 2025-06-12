using Microsoft.AspNetCore.Mvc;
using PracticeProject_DOTNET.Data;
using PracticeProject_DOTNET.Models;

namespace PracticeProject_DOTNET.Controllers
{
    public class CategoryController : Controller
    {

        public readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
            {
            if (ModelState.IsValid) { 
            _db.Categories.Add(obj);
            _db.SaveChanges();
            }
            return RedirectToAction("Index", "Category");
        }

       
        //Edit --- Get
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var CategoryfromDb = _db.Categories.FirstOrDefault(c => c.Id == id);

            if(CategoryfromDb == null) {
                return NotFound();    
            }


            return View(CategoryfromDb);
        }

        // EDIT - POST ✅ YOU NEEDED THIS PART
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }

            return View(category); // show validation messages
        }
    }
}
