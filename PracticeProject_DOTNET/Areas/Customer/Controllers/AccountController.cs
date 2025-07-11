using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // For session
using PracticeProject_DOTNET.DataAccess.Data;
using PracticeProject.Models;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login logic
        [HttpPost]
        public async Task<IActionResult> Login(string name, string password)
        {
            // ✅ 1. Check hardcoded admin first
            if (name == "adminuser" && password == "admin123")
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "adminuser"),
            new Claim(ClaimTypes.Role, "Admin")
        };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                HttpContext.Session.SetString("UserId", "0"); // 0 = dummy ID
                HttpContext.Session.SetString("UserRole", "Admin");

                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }

            // ✅ 2. Check in database
            var user = _db.ApplicationUsers
                .FirstOrDefault(u => u.Name == name && u.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Invalid Username or Password";
                return View();
            }

            var claimsDb = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.Role, user.Role)
    };

            var identityDb = new ClaimsIdentity(claimsDb, CookieAuthenticationDefaults.AuthenticationScheme);
            var principalDb = new ClaimsPrincipal(identityDb);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principalDb);

            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserRole", user.Role);

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
        [HttpGet]
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

        // ✅ Corrected Logout with SignOut
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
