using Invoice.DTOs;
using Invoice.Data.Entities;
using AutoMapper;

namespace Invoice.BAL.Mapper
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<UsersEntity, UsersDto>().ReverseMap();
        }
    }
}
