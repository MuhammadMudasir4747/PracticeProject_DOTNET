using Microsoft.EntityFrameworkCore;
using PracticeProject_DOTNET.Models;

namespace PracticeProject_DOTNET.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
    }
}
