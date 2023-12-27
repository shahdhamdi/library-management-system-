using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string PhoneNumber {  get; set; }

        [Required]
        public string Address { get; set; }
        public double totalPrice {  get; set; }

    }
}
