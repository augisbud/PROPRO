using InvoicesBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoicesBackend.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<Buyer> Buyers { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
    }
}