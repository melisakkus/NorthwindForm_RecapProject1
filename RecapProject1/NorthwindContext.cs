using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using RecapProject1.Entities2; // DbContext'ten inherite edilebilmesi için eklenmeli

namespace RecapProject1
{
    public class NorthwindContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}



