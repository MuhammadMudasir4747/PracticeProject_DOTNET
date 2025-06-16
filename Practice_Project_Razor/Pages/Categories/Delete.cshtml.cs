using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Practice_Project_Razor.Data;
using Practice_Project_Razor.Models;
using System.Threading.Tasks;

namespace Practice_Project_Razor.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Category Category { get; set; }

        // Load category data for confirmation
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category = await _db.Categories.FindAsync(id);
            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        // Handle delete after confirmation
        public async Task<IActionResult> OnPostAsync()
        {
            if (Category == null || Category.Id == 0)
            {
                return BadRequest();
            }

            var categoryToDelete = await _db.Categories.FindAsync(Category.Id);
            if (categoryToDelete == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(categoryToDelete);
            await _db.SaveChangesAsync();

            TempData["success"] = "Category Deleted Successfully";
            return RedirectToPage("Index");
        }
    }
}
