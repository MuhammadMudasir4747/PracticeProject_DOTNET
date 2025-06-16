using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Practice_Project_Razor.Data;
using Practice_Project_Razor.Models;
using System.Threading.Tasks;

namespace Practice_Project_Razor.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Category Category{ get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;    
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
               _db.Categories.Add(Category);
                await _db.SaveChangesAsync();

                TempData["success"] = "Category Created Successfully";
                return RedirectToPage("Index");
            }
            return Page();

        }
    }
}
