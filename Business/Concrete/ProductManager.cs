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

        public string? Message { get ; set ; }

        public async Task<bool> CreateAsync(ProductModel model, int[] categoriesIds)
        {
            if(categoriesIds.Length <1 )
            {
                Message = "Lütfen en az bir kategori ekleyin";
                return false;
            }
            
            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Url = UrlGenerator.Create(model.Name!),
                IsApproved = model.IsApproved,
                IsHome = model.IsHome,
                StockQuantity = model.StockQuantity,
                ProductCategories = categoriesIds!.Select(pc => new ProductCategory{
                    ProductId = model.Id,
                    CategoryId = pc
                }).ToList()
            };

            if(model!.Image == null)
            {
                product.ImageUrl = "noContent.jpg";
            }
            else
            {
                var extention = Path.GetExtension(model!.Image!.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                var path = Path.Combine(Directory.GetCurrentDirectory(),"..\\Presentation\\wwwroot\\images", randomName);

                using (var stream = new FileStream(path,FileMode.Create))
                {
                    await model!.Image.CopyToAsync(stream);
                }
                
                product.ImageUrl = randomName;           
            } 
            
            await _unitOfWork!.Products.CreateAsync(product);
            Message = "Ürün eklendi";

            return true;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _unitOfWork!.Products.GetByIdAsync(id);
            if(product is null)
            {
                Message = "Ürün bulunamadı";
                return false;
            }

            await _unitOfWork.Products.DeleteAsync(product);
            Message = "Ürün silindi";
            return true;
        }


        public async Task<ProductListViewModel> GetAllProductsAsync(int page)
        {
            var pageSize = Int32.Parse(_configuration["PageSize"]!);
            var products = await _unitOfWork!.Products.GetAllProductsAsync(page,pageSize);
            var productModels = products.Select(e=> _mapper.Map<ProductViewModel>(e));
            var productsCount = await _unitOfWork.Products.GetAllProductsCountAsync();

            return new ProductListViewModel
            {
                Products = productModels.ToList(),
                PageInfo = new PageInfoModel {
                    TotalItems = productsCount,
                    ItemPerPage = pageSize,
                    CurrentPage = page,
                    PaginationType = "AdminProductList"
                }
            };
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

        public async Task<ProductViewModel?> GetProductById(int id)
        {
            var product = await _unitOfWork!.Products.GetByIdAsync(id);
            return _mapper.Map<ProductViewModel>(product);
        }


        public async Task<ProductDetailsModel?> GetProductDetails(string url)
        {
            var product = await _unitOfWork!.Products.GetProductDetails(url);
            var productModel = _mapper.Map<ProductViewModel>(product);

            return new ProductDetailsModel
            {
                Product = productModel,
                Categories = product!.ProductCategories!.Select(e=>_mapper.Map<CategoryViewModel>(e.Category)).ToList()
            };
        }

        public async Task<ProductModel?> GetProductForUpdate(int id)
        {
            var product = await _unitOfWork!.Products.GetProductForUpdate(id);
            var model = new ProductModel
            {
                Id = product!.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Url = product.Url,
                StockQuantity = product.StockQuantity,
                Weight = product.Weight,
                IsApproved = product.IsApproved,
                IsHome = product.IsHome,
                SelectedCategories = product.ProductCategories!.Select(e=> _mapper.Map<CategoryViewModel>(e.Category)).ToList()
            };

            return model;
        }

        public async Task<ProductListViewModel> GetProductListByIds(IEnumerable<int> ids)
        {
            var products = await _unitOfWork!.Products.GetProductListByIds(ids);
            var productModels = products.Select(e=> _mapper.Map<ProductViewModel>(e));

            return new ProductListViewModel{
                Products = productModels.ToList()
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

        public async Task<ProductListViewModel> GetSearchResult(string query, int page)
        {
            var pageSize = Int32.Parse(_configuration["PageSize"]!);
            var products = await _unitOfWork!.Products.GetSearchResult(query,page,pageSize);
            var productModels = products.Select(p => _mapper.Map<ProductViewModel>(p));
            var productsCount = await _unitOfWork.Products.GetSearchResultCount(query);

            return new ProductListViewModel
            {
                Products = productModels.ToList(),
                PageInfo = new PageInfoModel {
                    TotalItems = productsCount,
                    ItemPerPage = pageSize,
                    CurrentPage = page,
                    SearchQuery = query,
                    PaginationType = "SearchProducts"
                }
            };
        }

        public async Task<bool> UpdateProductAsync(ProductModel model, int[] categoriesIds)
        {
            if(categoriesIds.Length == 0)
            {
                Message = "Lütfen en az bir kategori seçin";
                return false;
            }

            if(model!.Image is not null)
            {
                var extention = Path.GetExtension(model!.Image!.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                var path = Path.Combine(Directory.GetCurrentDirectory(),"..\\Presentation\\wwwroot\\images", randomName);

                using (var stream = new FileStream(path,FileMode.Create))
                {
                    await model!.Image.CopyToAsync(stream);
                }

                if(model!.ImageUrl != "noContent.jpg")
                {
                    var exPath = Path.Combine(Directory.GetCurrentDirectory(),"..\\Presentation\\wwwroot\\images",model!.ImageUrl!);
                    System.IO.File.Delete(exPath);

                    model.ImageUrl = randomName;
                }
                
                model.ImageUrl = randomName;           
            } 

            await _unitOfWork!.Products.UpdateProductAsync(model,categoriesIds);
            Message = "Ürün güncellendi";
            return true;
        }
    }
}