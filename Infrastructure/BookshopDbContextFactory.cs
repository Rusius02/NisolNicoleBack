using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure
{
    public class BookshopDbContextFactory : IDesignTimeDbContextFactory<BookshopDBContext>
    {
        public BookshopDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookshopDBContext>();

            optionsBuilder.UseSqlServer("Server=tcp:serveradri.database.windows.net,1433;Initial Catalog=NisolNicole;Persist Security Info=False;User ID=sqladmin;Password=Ugrpouimpl258S3;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            return new BookshopDBContext(optionsBuilder.Options);
        }
    }
}
