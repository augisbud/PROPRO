namespace InvoicesBackend.Models
{
    public class InvoiceDTO
    {
        public int Id;
        public string Name;
        public BuyerDTO Buyer;
        public double Discount; 
        public IEnumerable<ProductDTO> Products;
        public DateTimeOffset CreationDate;

        public InvoiceDTO(int id, string name, BuyerDTO buyer, double discount, List<ProductDTO> products, DateTimeOffset creationDate)
        {
            Id = id;
            Name = name;
            Buyer = buyer;
            Discount = discount;
            Products = products;
            CreationDate = creationDate;
        }
    }
}