namespace LibraryManagementSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string PhoneNumber {  get; set; }    
        public string Address { get; set; }
        public double totalPrice {  get; set; }

    }
}
