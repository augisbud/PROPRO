namespace InvoicesBackend.Models
{
    public class ProductDTO
    {
        public string Name;
        public int Quantity;
        public string Unit;
        public double Price;

        public ProductDTO(string name, int quantity, string unit, double price)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Price = price;
        }
    }
}