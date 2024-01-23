using AutoMapper;
using Business.Abstract;
using Data.Abstract;
using Entity;
using Microsoft.Extensions.Configuration;
using Shared.Models;
using Shared.ViewModels;

namespace Business.Concrete
{
    public class OrderManager: IOrderService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public OrderManager(IUnitOfWork? unitOfWork,IMapper mapper,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task CreateAsync(OrderModel model,string userId)
        {
            var order = new Order
            {
                OrderNumber = new Random().Next(111111, 999999).ToString(),
                OrderState = "Beklemede",
                PaymentType = "Kredi Kartı",
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                City = model.City,
                UserId = userId,
                Phone = model.Phone.ToString(),
                Email = model.Email,
                Note = model.Note,
                ConversationId = model.ConversationId,
                PaymentId = model.PaymentId,

                OrderItems = []
            };
            foreach (var item in model.CartModel!.CartItems!)
            {
                var orderItem = new OrderItem()
                {
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId
                };

                order.OrderItems.Add(orderItem);
            }

            await _unitOfWork!.Orders.CreateAsync(order);

        }

    }
}