using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; // For async methods like ToListAsync
using Practice_Project_Razor.Data;
using Practice_Project_Razor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Practice_Project_Razor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        // This property holds the list of categories to display on the page
        public List<Category> CategoryList { get; set; }

        // Constructor receives the database context via dependency injection
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        // Called when the page is first loaded (HTTP GET)
        public async Task OnGetAsync()
        {
            // Fetch all categories from the database asynchronously
            CategoryList = await _db.Categories.ToListAsync();
        }

        // This method handles a DELETE request triggered from a form on the same page
        //public async Task<IActionResult> OnPostDeleteAsync(int id)
        //{
        //    var category = await _db.Categories.FindAsync(id);

        //    if (category == null)
        //    {
        //        // If no category was found with the given ID, return a 404 Not Found
        //        return NotFound();
        //    }

        //    // Delete the category
        //    _db.Categories.Remove(category);
        //    await _db.SaveChangesAsync();

        //    // Store a success message in TempData to show on the page
        //    TempData["success"] = "Category deleted successfully.";

        //    // Reload the page to update the list
        //    return RedirectToPage();
        //}
    }
}
