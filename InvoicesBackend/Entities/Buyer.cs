using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoicesBackend.Entities
{
    public class Buyer
    {
        [Key]
        public Guid Id { get; set; }
        public int InvoiceId { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public string VAT { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        public virtual Invoice Invoice { get; set; } = null!;

        public Buyer() { }

        public Buyer(Guid id, int invoiceId, string name, string code, string vat, string address, string email, string phone)
        {
            Id = id;
            InvoiceId = invoiceId;
            Name = name;
            Code = code;
            VAT = vat;
            Address = address;
            Email = email;
            Phone = phone;
        }
    }
}
