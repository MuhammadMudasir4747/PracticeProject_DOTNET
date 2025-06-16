using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Practice_Project_Razor.Data;
using Practice_Project_Razor.Models;

namespace Practice_Project_Razor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Category> CategoryList { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            _db =  db;
        }
        public void OnGet()
        {
            CategoryList = _db.Categories.ToArray().ToList();

        }
    }
}
