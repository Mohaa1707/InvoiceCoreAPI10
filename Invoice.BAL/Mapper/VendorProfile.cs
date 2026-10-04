using Invoice.DTOs;
using Invoice.Data.Entities;
using AutoMapper;

namespace Invoice.BAL.Mapper
{
    public class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<VendorEntity, VendorDto>().ReverseMap();
        }
    }
}
