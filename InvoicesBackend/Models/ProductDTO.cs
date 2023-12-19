namespace InvoicesBackend.Models
{
    public class ProductDTO(string name, int quantity, string unit, double price)
    {
        public string Name { get; set; } = name;
        public int Quantity { get; set; } = quantity;
        public string Unit { get; set; } = unit;
        public double Price { get; set; } = price;
    }
}
