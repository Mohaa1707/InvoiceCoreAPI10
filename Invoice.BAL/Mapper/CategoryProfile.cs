using AutoMapper;
using Invoice.DTOs;
using Invoice.Data.Entities;

namespace Invoice.BAL.Mapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryEntity, CategoryDto>().ReverseMap();
        }
    }
}
