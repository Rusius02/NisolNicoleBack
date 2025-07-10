using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class BookshopDBContext : DbContext
    {
        public BookshopDBContext(DbContextOptions<BookshopDBContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderBook> OrderBooks { get; set; } = null!;
        public DbSet<SiteVisit> SiteVisits { get; set; } = null!;
        public DbSet<WritingEvent> WritingEvents { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Table mapping
            modelBuilder.Entity<Users>().ToTable("users");
            modelBuilder.Entity<Book>().ToTable("book");
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<OrderBook>().ToTable("order_books");
            modelBuilder.Entity<SiteVisit>().ToTable("site_visits"); // nom fictif
            modelBuilder.Entity<WritingEvent>().ToTable("writing_event");

            // Users mapping
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).HasColumnName("idUser");
                entity.Property(u => u.FirstName).HasColumnName("first_name");
                entity.Property(u => u.LastName).HasColumnName("last_name");
                entity.Property(u => u.Sexe).HasColumnName("sexe");
                entity.Property(u => u.BirthDate).HasColumnName("birthdate");
                entity.Property(u => u.Mail).HasColumnName("mail");
                entity.Property(u => u.Pseudo).HasColumnName("pseudo");
                entity.Property(u => u.Password).HasColumnName("password");
                entity.Property(u => u.Role).HasColumnName("role");
                entity.Property(u => u.AddressStreet).HasColumnName("address_street");
                entity.Property(u => u.AddressNumber).HasColumnName("address_number");
                entity.Property(u => u.AddressCity).HasColumnName("address_city");
                entity.Property(u => u.AddressZip).HasColumnName("address_zip");
                entity.Property(u => u.AddressCountry).HasColumnName("address_country");
            });

            // Book mapping
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Id).HasColumnName("idBook");
                entity.Property(b => b.Title).HasColumnName("name");
                entity.Property(b => b.Description).HasColumnName("description");
                entity.Property(b => b.Price).HasColumnName("price");
                entity.Property(b => b.ISBN).HasColumnName("isbn");
                entity.Property(b => b.CoverImagePath).HasColumnName("CoverImagePath");
                entity.Property(b => b.StripeProductId).HasColumnName("stripeProductId");
            });

            // Order mapping
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.OrderId);
                entity.Property(o => o.OrderId).HasColumnName("orderId");
                entity.Property(o => o.UserId).HasColumnName("userId");
                entity.Property(o => o.Amount).HasColumnName("amount");
                entity.Property(o => o.PaymentStatus).HasColumnName("paymentStatus");
                entity.Property(o => o.StripePaymentIntentId).HasColumnName("stripePaymentIntentId");
                entity.Property(o => o.CreatedAt).HasColumnName("createdAt");

                // Foreign key to Users
                entity.HasOne(o => o.User)
                      .WithMany()
                      .HasForeignKey(o => o.UserId);
            });

            // OrderBook mapping
            modelBuilder.Entity<OrderBook>(entity =>
            {
                entity.HasKey(ob => new { ob.OrderId, ob.BookId });

                entity.Property(ob => ob.OrderId).HasColumnName("orderId");
                entity.Property(ob => ob.BookId).HasColumnName("bookId");

                entity.HasOne(ob => ob.Book)
                      .WithMany()
                      .HasForeignKey(ob => ob.BookId);

                entity.HasOne<Order>()
                      .WithMany(o => o.OrderBooks)
                      .HasForeignKey(ob => ob.OrderId);
            });

            // WritingEvent mapping
            modelBuilder.Entity<WritingEvent>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("idWritingEvent");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Theme).HasColumnName("theme");
                entity.Property(e => e.StartDate).HasColumnName("start_date");
                entity.Property(e => e.EndDate).HasColumnName("end_date");
            });

            // SiteVisit table (si existante)
            modelBuilder.Entity<SiteVisit>(entity =>
            {
                entity.ToTable("site_visits");
                entity.HasKey(v => v.Id);
                entity.Property(v => v.Id).HasColumnName("id");
                entity.Property(v => v.VisitedAt).HasColumnName("visited_at");
                entity.Property(v => v.IpAddress).HasColumnName("ip_address");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
