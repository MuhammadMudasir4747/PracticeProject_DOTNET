using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Practice_Project_Razor.Data;
using Practice_Project_Razor.Models;
using System.Threading.Tasks;

namespace Practice_Project_Razor.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
       

        public EditModel(ApplicationDbContext db)
        {
            _db = db;   
        }
        [BindProperty]
        public Category Category{ get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category = await _db.Categories.FindAsync(id);
            if (Category == null) { 
            return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                _db.Update(Category);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category Edited Successfully";
                return RedirectToPage("Index");
            }
            return Page();
          
        }
        
    }
}
