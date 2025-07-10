using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Required for HttpContext.Session
using PracticeProject_DOTNET.DataAccess.Data;
using PracticeProject.Models;
using System.Linq;

namespace PracticeProject_DOTNET.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AccountController : Controller
    {
        private readonly MyNewDbContext _db;

        public AccountController(MyNewDbContext db)
        {
            _db = db;
        }

        // GET: Login page
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login logic
        [HttpPost]
        public IActionResult Login(string name, string password)
        {
            var user = _db.ApplicationUsers
                .FirstOrDefault(u => u.Name == name && u.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Invalid Username or Password";
                return View();
            }

            // Store user info in session
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserRole", user.Role);

            // Redirect based on role
            if (user.Role == "Admin")
            {
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
        }

        // GET: Signup page
        public IActionResult Signup()
        {
            return View();
        }

        // POST: Signup logic
        [HttpPost]
        public IActionResult Signup(ApplicationUser user)
        {
            var exists = _db.ApplicationUsers.Any(u => u.Name == user.Name);
            if (exists)
            {
                ViewBag.Message = "Username already taken";
                return View();
            }

            user.Role = "Customer"; // Always customer
            _db.ApplicationUsers.Add(user);
            _db.SaveChanges();

            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserRole", user.Role);

            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        // Logout action
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
