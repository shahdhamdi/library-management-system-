using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data;

public class ApplicationDbContext :IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
    { 
    }

    public DbSet<Book>Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CartItem> ShoppingCartItems { get; set; }
    public DbSet<Cart> carts { get; set; }
    public DbSet<Order>? Order { get; set; }


}
