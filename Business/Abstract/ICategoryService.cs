using Shared.Models;
using Shared.ViewModels;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<CategoryListViewModel> GetAllAsync();
    }
}