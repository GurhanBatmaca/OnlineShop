using Entity;
using Shared.Models;

namespace Data.Abstract
{
    public interface ICategoryRepository: IRepository<Category>
    {
        Task UpdateCategoryAsync(CategoryModel model);
    }
}