using System.ComponentModel.DataAnnotations;

namespace InvoicesBackend.Entities
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Discount { get; set; }
        public DateTimeOffset CreationDate { get; private set; }

        public Buyer Buyer { get; set; }
        public ICollection<Product> Products { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Invoice()
        {
            CreationDate = DateTimeOffset.UtcNow;
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Invoice(int id, string name, double discount) : this()
        {
            Id = id;
            Name = name;
            Discount = discount;
        }
    }
}
