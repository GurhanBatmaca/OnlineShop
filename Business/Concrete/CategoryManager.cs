using AutoMapper;
using Business.Abstract;
using Data.Abstract;
using Entity;
using Microsoft.Extensions.Configuration;
using Shared.Helpers;
using Shared.Models;
using Shared.ViewModels;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly IMapper _mapper;

        private readonly IConfiguration _configuration;
        public CategoryManager(IUnitOfWork? unitOfWork,IMapper mapper,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public string? Message { get ; set; }


        public async Task<bool> CreateAsync(CategoryModel model)
        {
            var categories = await _unitOfWork!.Categories.GetAllAsync();
            foreach (var category in categories)
            {
                if(category.Name!.ToLower() == model.Name!.ToLower())
                {
                    Message = "Bu kategori adı mevcut.";
                    return false;
                }
            }
            
            var entity = new Category
            {
                Name = model.Name,
                Url = UrlGenerator.Create(model.Name!)
            };

            await _unitOfWork.Categories.CreateAsync(entity);
            Message = $"{entity.Name} kategorilere eklendi.";
            return true;
        }


        public async Task<CategoryListViewModel> GetAllAsync()
        {
            var categories = await _unitOfWork!.Categories.GetAllAsync();
            var categoryModels = categories.Select(p => _mapper.Map<CategoryViewModel>(p));

            return new CategoryListViewModel
            {
               Categories = categoryModels.ToList()
            };
        }

        public async Task<CategoryModel> GetByIdAsync(int id)
        {
            var category = await _unitOfWork!.Categories.GetByIdAsync(id);

            return _mapper.Map<CategoryModel>(category);
        }

        public async Task<bool> UpdateAsync(CategoryModel model)
        {
            var categories = await _unitOfWork!.Categories.GetAllAsync();
            foreach (var category in categories)
            {
                if(category.Name!.ToLower() == model.Name!.ToLower())
                {
                    Message = "Bu kategori adı mevcut.";
                    return false;
                }
            }
          
            await _unitOfWork!.Categories.UpdateCategoryAsync(model);
            Message = $"{model.Name} isimli kategori güncellendi.";
            return true;
        }

    }
}