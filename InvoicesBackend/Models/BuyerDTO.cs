namespace InvoicesBackend.Models
{
    public class BuyerDTO(string name, string code, string vat, string address, string email, string phone)
    {
        public string Name { get; set; } = name;
        public string Code { get; set; } = code;
        public string VAT { get; set; } = vat;
        public string Address { get; set; } = address;
        public string Email { get; set; } = email;
        public string Phone { get; set; } = phone;
    }
}
