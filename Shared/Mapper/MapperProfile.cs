using AutoMapper;
using Entity;
using Shared.Models;
using Shared.ViewModels;

namespace Shared.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Product,ProductViewModel>();
            CreateMap<Category,CategoryViewModel>();
            CreateMap<Category,CategoryModel>();

            CreateMap<CategoryModel,Category>();
            CreateMap<ProductViewModel,Product>();
        }
    }
}