using AutoMapper;
using Entity;
using Shared.ViewModels;

namespace Shared.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Product,ProductViewModel>();
            CreateMap<Category,CategoryViewModel>();
        }
    }
}