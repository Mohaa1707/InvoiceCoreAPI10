using AutoMapper;
using InvoiceCoreAPI.Entities;
using InvoiceCoreAPI.DTO;
namespace InvoiceCoreAPI.Mapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
