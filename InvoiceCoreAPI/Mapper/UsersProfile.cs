using AutoMapper;
using InvoiceCoreAPI.DTO;
using InvoiceCoreAPI.Entities;

namespace InvoiceCoreAPI.Mapper
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<Users, UsersDto>().ReverseMap();
        }
    }
}
