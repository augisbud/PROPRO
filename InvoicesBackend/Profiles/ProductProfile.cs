using AutoMapper;

namespace InvoicesBackend.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Entities.Product, Models.ProductDTO>();
            CreateMap<Models.ProductDTO, Entities.Product>()
                .ForCtorParam("InvoiceId", opt => opt.MapFrom(src => 0));
        }
    }
}