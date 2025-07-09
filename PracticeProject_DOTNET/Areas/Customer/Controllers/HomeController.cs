using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NuGet.ProjectModel;
using Optivem.Framework.Core.Domain;
using PracticeProject.Models;
using PracticeProject.DataAccess.Repository.IRepository;
using IUnitOfWork = PracticeProject.DataAccess.Repository.IRepository.IUnitOfWork;




namespace PracticeProject_DOTNET.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork) : Controller

    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public IActionResult Index()
        {
            IEnumerable <Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            return View(productList);
        }

        public IActionResult Details(int id)
        {
            var Product = _unitOfWork.Product.Get(u=> u.Id == id ,includeProperties: "Category");
            if(Product == null)
            {
                return NotFound();
            }
            return View(Product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
