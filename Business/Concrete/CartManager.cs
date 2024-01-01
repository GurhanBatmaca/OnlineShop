using AutoMapper;
using Business.Abstract;
using Data.Abstract;
using Entity;
using Microsoft.Extensions.Configuration;
using Shared.ViewModels;

namespace Business.Concrete
{
    public class CartManager: ICartService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public CartManager(IUnitOfWork? unitOfWork,IMapper mapper,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task AddToCartAsync(string userId, int productId, int quantity)
        {
            await _unitOfWork!.Carts.AddToCartAsync(userId,productId,quantity);
        }


        public async Task CreateAsync(Cart cart)
        {
            await _unitOfWork!.Carts.CreateAsync(cart);
        }

        public async Task<CartViewModel?> GetCartByUserId(string userId)
        {
            var cart = await _unitOfWork!.Carts.GetCartByUserId(userId);

            CartViewModel model = new CartViewModel
            {
                 Id = cart!.Id,
                UserId = cart.UserId,
                CartItems = cart.CartItems!.Select(e=> new CartItemViewModel
                {
                    CartItemId = e.Id,
                    ProductId = e.ProductId,
                    Name = e.Product!.Name,
                    Price = e.Product.Price,
                    ImageUrl = e.Product.ImageUrl,
                    Quantity = e.Quantity
                }).ToList()
            };


            return model;
        }
    }
}