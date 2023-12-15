using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents
{
    public class CategoriesViewComponent: ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.SelectedCategory = RouteData.Values["kategori"];
            var model = await _categoryService.GetAllAsync();
            return View(model);
        }
    }
}