using System.ComponentModel.DataAnnotations;

namespace PracticeProject_DOTNET.Models
{
    public class Category
    {
        public int Id { get; set; } //primary key for the table

        [Required]
        public string Name { get; set; }

        public int DisplayOrder { get; set; }
    }
}
 