using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Practice_DOTNET_RAZOR_TEMP.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public String Name { get; set; }

        [DisplayName("Display Ordeer")]
        [Range(1, 100, ErrorMessage ="Display Order must be between 1 and 100")]
        public int DisplayOrder{ get; set; }
    }
}
