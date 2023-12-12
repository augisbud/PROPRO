using InvoicesBackend.DbContexts;
using InvoicesBackend.Entities;
using InvoicesBackend.Models;

namespace InvoicesBackend.Services
{
    public class InvoicesRepository(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public Invoice AddInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            return invoice;
        }

        public Invoice? GetInvoiceById(int id)
        {
            return _context.Invoices.Where(i => i.Id == id).FirstOrDefault();
        }
    }
}