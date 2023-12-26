using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }

        public string Description { get; set; }

        [Display(Name="Published Date")]
        public string publishedDate { get; set; }

        public int Quantity { get; set; }

        [Display(Name="Book Cover")]
        public string BookImage { get; set; }
        [Display(Name="Category")]
        public int CategoryId { get; set; } 
        public Category Category { get; set; }
    }
}
