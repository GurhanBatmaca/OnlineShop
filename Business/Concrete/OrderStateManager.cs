using AutoMapper;
using Business.Abstract;
using Data.Abstract;
using Entity;
using Microsoft.Extensions.Configuration;
using Shared.Models;
using Shared.ViewModels;

namespace Business.Concrete
{
    public class OrderStateManager: IOrderStateService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public OrderStateManager(IUnitOfWork? unitOfWork,IMapper mapper,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Task CreateAsync(OrderState orderState)
        {
            throw new NotImplementedException();
        }

    }
}