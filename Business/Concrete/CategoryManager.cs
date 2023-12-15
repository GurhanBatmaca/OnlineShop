using AutoMapper;
using Business.Abstract;
using Data.Abstract;
using Microsoft.Extensions.Configuration;
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

        public async Task<CategoryListViewModel> GetAllAsync()
        {
            var categories = await _unitOfWork!.Categories.GetAllAsync();
            var categoryModels = categories.Select(p => _mapper.Map<CategoryViewModel>(p));

            return new CategoryListViewModel
            {
               Categories = categoryModels.ToList()
            };
        }

    }
}