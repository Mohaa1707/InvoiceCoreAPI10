using AutoMapper;
using Invoice.DTOs;
using Invoice.Data.Entities;

namespace Invoice.BAL.Mapper
{
    public class ItemMasterProfile : Profile
    {
        public ItemMasterProfile()
        {
            CreateMap<ItemmasterEntity, ItemmasterDto>().ReverseMap();
        }
    }
}
