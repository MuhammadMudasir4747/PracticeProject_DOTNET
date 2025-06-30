using Microsoft.AspNetCore.Mvc;
using PracticeProject.DataAccess.Repository.IRepository;
using PracticeProject.Models;
using PracticeProject_DOTNET.DataAccess.Data;




namespace PracticeProject_DOTNET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }
        public IActionResult Create() { 
        return View();
        }
        [HttpPost]
        public IActionResult Create(Product product) {
            if (ModelState.IsValid) { 
            _unitOfWork.Product.Add(product);
                _unitOfWork.Save();
                TempData["success"] = "Product Created  Successfully";
            }
            return RedirectToAction("Index", "Product");
        }


        //Get Edit
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var productFromDb = _unitOfWork.Product.Get(p => p.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }


        // EDIT - POST ✅ YOU NEEDED THIS PART
        [HttpPost]
        public IActionResult Edit(Product product) {
            if (ModelState.IsValid) { 
            _unitOfWork.Product.Update(product);
                _unitOfWork.Save();
                TempData["success"] = "Product updated Successfully";
                return RedirectToAction("Index", "Product");
            }

            
            return RedirectToAction("Index", "Product");
        }

        //Delete method
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            var products_received = _unitOfWork.Product.Get(c => c.Id == id);
            if (products_received == null) { return NotFound(); }
            //return View(catagories_received);
            _unitOfWork.Product.Remove(products_received);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted Successfully";
            return RedirectToAction("Index", "Product");
        }
    }

}
    

