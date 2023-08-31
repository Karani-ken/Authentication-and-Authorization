using Authentication_and_Authorization.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication_and_Authorization.Data
{
    public class AppDbContext:DbContext
    {
      public  DbSet<User> Users { get; set; }
      public  DbSet<Product> Products { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
