namespace InvoicesBackend.Models
{
    public class InvoiceForCreationDTO
    {
        public string Name;
        public BuyerDTO Buyer;
        public double Discount; 
        public IEnumerable<ProductDTO> Products;

        public InvoiceForCreationDTO(string name, BuyerDTO buyer, double discount, List<ProductDTO> products)
        {
            Name = name;
            Buyer = buyer;
            Discount = discount;
            Products = products;
        }
    }
}