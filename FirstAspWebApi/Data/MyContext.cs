using FirstAspWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstAspWebApi.Data
{
    public class MyContext:DbContext
    {
      public  DbSet<User> users { get; set; }

      public DbSet<Contact> contacts { get; set; }

        public DbSet<ContactDetail > Details { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options) { } 



    }
}
