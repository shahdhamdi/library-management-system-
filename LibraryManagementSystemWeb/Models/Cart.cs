using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Cart
    {
       public int Id { get; set; }
   
        public string BuyStatus { get; set; }
        public List<Book> cart { get; set; }
        public double TotalPrice { get; set; }

    }
}
