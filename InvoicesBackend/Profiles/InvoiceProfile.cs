using AutoMapper;

namespace InvoicesBackend.Profiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Entities.Invoice, Models.InvoiceDTO>();
            CreateMap<Models.InvoiceDTO, Entities.Invoice>();

            CreateMap<Models.InvoiceForCreationDTO, Entities.Invoice>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Buyer, opt => opt.MapFrom(src => src.Buyer))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
                .ForMember(dest => dest.CreationDate, opt => opt.Ignore());
                
        }
    }
}