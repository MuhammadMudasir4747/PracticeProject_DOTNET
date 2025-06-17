using System.ComponentModel.DataAnnotations;

namespace PracticeProject.Models
{
    public class Category
    {
        public int Id { get; set; } //primary key for the table

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Range(1,100)]  
        public int DisplayOrder { get; set; }
    }
}
 