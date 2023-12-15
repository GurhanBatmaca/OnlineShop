using AutoMapper;
using Business.Abstract;
using Data.Abstract;
using Shared.Models;
using Shared.ViewModels;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly IMapper _mapper;
        public ProductManager(IUnitOfWork? unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ProductListViewModel> GeTHomePageProducts(int page)
        {
            var pageSize = 12;
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

    }
}