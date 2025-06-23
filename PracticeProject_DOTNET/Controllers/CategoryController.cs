using Microsoft.AspNetCore.Mvc;
using PracticeProject.DataAccess.Repository.IRepository;
using PracticeProject.Models;
using PracticeProject_DOTNET.DataAccess.Data;

namespace PracticeProject_DOTNET.Controllers
{
    public class CategoryController : Controller
    {

        public readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
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
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
            TempData["success"] = "Category Created  Successfully";
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

            var CategoryfromDb = _unitOfWork.Category.Get(c => c.Id == id);

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
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category updated Successfully";
                return RedirectToAction("Index", "Category");

                
            }

            //return View(category); // show validation messages
            return RedirectToAction("Index", "Category");
        }

        //Delete method
        public IActionResult Delete(int? id)
        {
            if(id==null || id == 0){ return NotFound();}
            var catagories_received = _unitOfWork.Category.Get(c => c.Id == id);
            if (catagories_received == null) { return NotFound(); }
            //return View(catagories_received);
            _unitOfWork.Category.Remove(catagories_received);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted Successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
