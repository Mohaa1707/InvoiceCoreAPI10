using AutoMapper;
using Invoice.DTOs;
using Invoice.Data.Entities;

namespace Invoice.BAL.Mapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerEntity, CustomerDto>().ReverseMap();
        }
    }
}
