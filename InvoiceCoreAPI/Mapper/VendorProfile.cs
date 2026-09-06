using AutoMapper;
using InvoiceCoreAPI.Entities;
using InvoiceCoreAPI.DTO;

namespace InvoiceCoreAPI.Mapper
{
    public class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<Vendor, VendorDto>().ReverseMap();
        }
    }
}