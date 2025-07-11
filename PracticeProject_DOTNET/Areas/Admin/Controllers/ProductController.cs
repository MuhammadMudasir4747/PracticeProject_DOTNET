using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]  // Only allow Admin role
    public class ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) : Controller
    {
        public readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public IActionResult Index()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return View(productList);
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

                if (productVM.Product.Id == 0)
                {
                    // ---------- CREATE ----------
                    if (file != null)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string productPath = Path.Combine(wwwRootPath, @"images\products");

                        if (!Directory.Exists(productPath))
                        {
                            Directory.CreateDirectory(productPath);
                        }

                        using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        productVM.Product.ImageUrl = @"\images\products\" + fileName;
                    }

                    _unitOfWork.Product.Add(productVM.Product);
                    TempData["success"] = "Product Created Successfully";
                }
                else
                {
                    // ---------- UPDATE ----------
                    var productFromDb = _unitOfWork.Product.Get(u => u.Id == productVM.Product.Id);

                    if (productFromDb != null)
                    {
                        if (file != null)
                        {
                            if (!string.IsNullOrEmpty(productFromDb.ImageUrl))
                            {
                                var oldImagePath = Path.Combine(wwwRootPath, productFromDb.ImageUrl.TrimStart('\\'));
                                if (System.IO.File.Exists(oldImagePath))
                                {
                                    System.IO.File.Delete(oldImagePath);
                                }
                            }

                            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            string productPath = Path.Combine(wwwRootPath, @"images\products");

                            if (!Directory.Exists(productPath))
                            {
                                Directory.CreateDirectory(productPath);
                            }

                            using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                            {
                                file.CopyTo(fileStream);
                            }

                            productFromDb.ImageUrl = @"\images\products\" + fileName;
                        }

                        // Update all other fields manually
                        productFromDb.Title = productVM.Product.Title;
                        productFromDb.Description = productVM.Product.Description;
                        productFromDb.Author = productVM.Product.Author;
                        productFromDb.ISBN = productVM.Product.ISBN;
                        productFromDb.ListPrice = productVM.Product.ListPrice;
                        productFromDb.Price = productVM.Product.Price;
                        productFromDb.Price50 = productVM.Product.Price50;
                        productFromDb.Price100 = productVM.Product.Price100;
                        productFromDb.CategoryId = productVM.Product.CategoryId;

                        TempData["success"] = "Product Updated Successfully";
                    }
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            // If invalid model state
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
    

