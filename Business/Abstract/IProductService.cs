using Shared.Models;
using Shared.ViewModels;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<ProductListViewModel> GeTHomePageProducts(int page);
    }
}