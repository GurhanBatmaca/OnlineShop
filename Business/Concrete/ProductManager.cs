using AutoMapper;
using Business.Abstract;
using Data.Abstract;
using Microsoft.Extensions.Configuration;
using Shared.Models;
using Shared.ViewModels;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly IMapper _mapper;

        private readonly IConfiguration _configuration;
        public ProductManager(IUnitOfWork? unitOfWork,IMapper mapper,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<ProductListViewModel> GetHomePageProducts(int page)
        {
            var pageSize = Int32.Parse(_configuration["PageSize"]!);
            var products = await _unitOfWork!.Products.GetHomePageProducts(page,pageSize);
            var productModels = products.Select(p => _mapper.Map<ProductViewModel>(p));
            var productsCount = await _unitOfWork.Products.GetHomePageProductsCount();

            return new ProductListViewModel
            {
                Products = productModels.ToList(),
                PageInfo = new PageInfoModel {
                    TotalItems = productsCount,
                    ItemPerPage = pageSize,
                    CurrentPage = page,
                    PaginationType = "HomePageProducts"
                }
            };
        }

        public async Task<ProductListViewModel> GetProductsByCategory(string category, int page)
        {
            var pageSize = Int32.Parse(_configuration["PageSize"]!);
            var products = await _unitOfWork!.Products.GetProdutsByCategory(category,page,pageSize);
            var productModels = products.Select(p => _mapper.Map<ProductViewModel>(p));
            var productsCount = await _unitOfWork.Products.GetProdutsCountByCategory(category);

            return new ProductListViewModel
            {
                Products = productModels.ToList(),
                PageInfo = new PageInfoModel {
                    TotalItems = productsCount,
                    ItemPerPage = pageSize,
                    CurrentPage = page,
                    CurrentCategory = category,
                    PaginationType = "ProductByCategory"
                }
            };
        }
    }
}