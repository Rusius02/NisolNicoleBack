using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class BookshopDbContextFactory : IDesignTimeDbContextFactory<BookshopDBContext>
    {
        public BookshopDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookshopDBContext>();

            // Indique ta chaîne de connexion ici
            optionsBuilder.UseSqlServer("Server=MSI;DataBase=NisolNicole;Integrated Security=SSPI");

            return new BookshopDBContext(optionsBuilder.Options);
        }
    }
}
