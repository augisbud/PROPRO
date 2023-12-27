namespace InvoicesBackend.Models
{
    public class InvoiceForCreationDTO(string name, BuyerDTO buyer, double discount, DateTimeOffset paymentDate, IEnumerable<ProductDTO> products)
    {
        public string Name { get; set; } = name;
        public BuyerDTO Buyer { get; set; } = buyer;
        public double Discount { get; set; } = discount;
        public DateTimeOffset PaymentDate { get; set; } = paymentDate;
        public IEnumerable<ProductDTO> Products { get; set; } = products;
    }
}
