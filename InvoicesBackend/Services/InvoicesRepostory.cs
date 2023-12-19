using InvoicesBackend.DbContexts;
using InvoicesBackend.Entities;
using InvoicesBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoicesBackend.Services
{
    public class InvoicesRepository(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public Invoice? GetInvoiceById(int id)
        {
            return _context.Invoices.Where(i => i.Id == id).Include(invoice => invoice.Buyer).Include(invoice => invoice.Products).FirstOrDefault();
        }

        public Invoice AddInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            return invoice;
        }

        public void RemoveInvoice(Invoice invoice)
        {
            _context.Invoices.Remove(invoice);
            _context.SaveChanges();
        }

        public List<Invoice> RetrieveInvoices(DateTimeOffset? startDate, DateTimeOffset? endDate)
        {
            var query = _context.Invoices.Include(invoice => invoice.Buyer).Include(invoice => invoice.Products).AsQueryable();

            if (startDate.HasValue)
                query = query.Where(invoice => invoice.CreationDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(invoice => invoice.CreationDate <= endDate.Value);

            return [.. query];
        }

    }
}