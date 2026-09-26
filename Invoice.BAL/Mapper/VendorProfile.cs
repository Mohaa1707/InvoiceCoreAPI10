using Invoice.DTOs;
using Invoice.Data.Entities;
using AutoMapper;

namespace Invoice.BAL.Mapper
{
    internal class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<VendorEntity, VendorDto>().ReverseMap();
        }
    }
}
