using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure
{
    public class BookshopDbContextFactory : IDesignTimeDbContextFactory<BookshopDBContext>
    {
        public BookshopDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookshopDBContext>();

            optionsBuilder.UseSqlServer("Server=MSI;DataBase=NisolNicole;Integrated Security=SSPI");

            return new BookshopDBContext(optionsBuilder.Options);
        }
    }
}
