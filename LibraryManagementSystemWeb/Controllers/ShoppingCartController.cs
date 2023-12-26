using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LibraryManagementSystem.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IHttpContextAccessor _context;
        private readonly ApplicationDbContext _db;

        public ShoppingCartController(ApplicationDbContext applicationDbContext,IHttpContextAccessor c)
        {
            _context = c;
            _db = applicationDbContext;
        }
        [Authorize]
        public IActionResult Index()
        {
            string cartString = _context.HttpContext.Session.GetString("Cart");
            if (cartString != null)
            {
                List<Book> Cart = JsonConvert.DeserializeObject<List<Book>>(cartString);

                double totalPrice = Cart.Sum(item => item.Price);
                var cart = new Cart
                {
                    cart = Cart,
                    TotalPrice = totalPrice
                };
                return View(cart);
           }
            var emptyCart = new Cart();
            

            return View(emptyCart);

        }
        [Authorize]
        public IActionResult AddToCart(int id)
        {
        
                var cartItem = _db.Books.Find(id);
                if (cartItem == null) return NotFound();

            if (_context.HttpContext.Session.Keys.Contains("Cart"))
            {
                string cartString1 = _context.HttpContext.Session.GetString("Cart");
                if (cartString1 != null)
                {
                    List<Book> Cart = JsonConvert.DeserializeObject<List<Book>>(cartString1);
                    Cart.Add(cartItem);

                    // Cart = Cart;
                    _context.HttpContext.Session.Remove("Cart");

                    cartString1 = JsonConvert.SerializeObject(Cart);
                    _context.HttpContext.Session.SetString("Cart", cartString1);
                }
            }
            else
            {
                List<Book> Cart = new List<Book>
                {
                    cartItem
                };

                string cartString = JsonConvert.SerializeObject(Cart);
                _context.HttpContext.Session.SetString("Cart", cartString);
            }
            return RedirectToAction("Index");
        }

    }
}
