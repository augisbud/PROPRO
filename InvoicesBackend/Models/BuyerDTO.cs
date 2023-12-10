namespace InvoicesBackend.Models
{
    public class BuyerDTO
    {
        public string Name;
        public string Code;
        public string VAT;
        public string Address;
        public string Email;
        public string Phone;

        public BuyerDTO(string name, string code, string vat, string address, string email, string phone)
        {
            Name = name;
            Code = code;
            VAT = vat;
            Address = address;
            Email = email;
            Phone = phone;
        }
    }
}