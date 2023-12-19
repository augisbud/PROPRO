namespace InvoicesBackend.Models
{
    public class InvoiceForCreationDTO(string name, BuyerDTO buyer, double discount, IEnumerable<ProductDTO> products)
    {
        public string Name { get; set; } = name;
        public BuyerDTO Buyer { get; set; } = buyer;
        public double Discount { get; set; } = discount;
        public IEnumerable<ProductDTO> Products { get; set; } = products;
    }
}
