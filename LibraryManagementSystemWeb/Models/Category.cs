using System.ComponentModel.DataAnnotations; 

namespace LibraryManagementSystem.Models
{
    public class Category
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        
        [Display(Name="description")]
        public string Describtion { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public ICollection<Book> Books { get; set;} = new List<Book>(); // Collection navigation containing dependents



    }
}
