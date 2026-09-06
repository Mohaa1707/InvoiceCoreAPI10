using AutoMapper;
using InvoiceCoreAPI.DTO;
using InvoiceCoreAPI.Entities;


namespace InvoiceCoreAPI.Mapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
