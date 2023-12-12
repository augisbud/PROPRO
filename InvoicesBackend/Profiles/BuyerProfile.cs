using AutoMapper;

namespace InvoicesBackend.Profiles
{
    public class BuyerProfile : Profile
    {
        public BuyerProfile()
        {
            CreateMap<Entities.Buyer, Models.BuyerDTO>();
            CreateMap<Models.BuyerDTO, Entities.Buyer>()
                .ForCtorParam("InvoiceId", opt => opt.MapFrom(src => 0));
        }
    }
}