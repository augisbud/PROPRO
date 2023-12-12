using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoicesBackend.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public int InvoiceId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        public virtual Invoice Invoice { get; set; } = null!;

        public Product()
        {
        }

        public Product(int invoiceId, Guid id, string name, int quantity, string unit, double price)
        {
            Id = id;
            InvoiceId = invoiceId;
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Price = price;
        }
    }
}
