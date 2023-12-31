using AutoMapper;
using Business.Abstract;
using Data.Abstract;
using Entity;
using Microsoft.Extensions.Configuration;

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

        public async Task CreateAsync(Cart cart)
        {
            await _unitOfWork!.Carts.CreateAsync(cart);
        }

    }
}