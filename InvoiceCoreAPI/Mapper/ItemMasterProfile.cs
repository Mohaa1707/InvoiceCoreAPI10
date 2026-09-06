using AutoMapper;
using InvoiceCoreAPI.Entities;
using InvoiceCoreAPI.DTO;
namespace InvoiceCoreAPI.Mapper
{
    public class ItemMasterProfile : Profile
    {
        public ItemMasterProfile()
        {
            CreateMap<Itemmaster, ItemmasterDto>().ReverseMap();
        }
    }
}
