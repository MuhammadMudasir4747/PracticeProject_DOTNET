using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PracticeProject.DataAccess.Repository.IRepository;
using PracticeProject.Models;
using PracticeProject.Models.ViewModels;
using PracticeProject_DOTNET.DataAccess.Data;
using System.Collections.Generic;




namespace PracticeProject_DOTNET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
         
            return View(objProductList);
        }
        public IActionResult Upsert(int? id) {


            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                Product = new Product()
            };
            if (id == null || id==0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
            
        }
        [HttpPost]
      
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                // Upload image if file is not null
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\products");

                    // Create directory if not exists
                    if (!Directory.Exists(productPath))
                    {
                        Directory.CreateDirectory(productPath);
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    // Save relative path to database
                    productVM.Product.ImageUrl = @"\images\products\" + fileName;
                }





                if (productVM.Product.Id == 0)
                {
                    // New product
                    _unitOfWork.Product.Add(productVM.Product);
                    TempData["success"] = "Product Created Successfully";
                }
                else
                {
                    // Existing product
                    _unitOfWork.Product.Update(productVM.Product);
                    TempData["success"] = "Product Updated Successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            // If ModelState is not valid, reload the CategoryList
            productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });

            return View(productVM);
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
    

