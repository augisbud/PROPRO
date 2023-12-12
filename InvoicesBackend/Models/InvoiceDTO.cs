namespace InvoicesBackend.Models
{
    public class InvoiceDTO(int id, string name, BuyerDTO buyer, double discount, IEnumerable<ProductDTO> products, DateTimeOffset creationDate)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public BuyerDTO Buyer { get; set; } = buyer;
        public double Discount { get; set; } = discount;
        public IEnumerable<ProductDTO> Products { get; set; } = products;
        public DateTimeOffset CreationDate { get; set; } = creationDate;
    }
}
