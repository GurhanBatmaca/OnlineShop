using Shared.Models;
using Shared.ViewModels;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        string? Message { get; set; }
        Task<CategoryListViewModel> GetAllAsync();
        Task<bool> CreateAsync(CategoryModel model);
        Task<CategoryModel> GetByIdAsync(int id);
        Task<bool> UpdateAsync(CategoryModel model);
    }
}