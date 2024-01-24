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

        public async Task<OrderListViewModel> GetOrders(string userId, int page)
        {
            var pageSize = Int32.Parse(_configuration["PageSize"]!);
            var orders = await _unitOfWork!.Orders.GetOrders(userId,page,pageSize);
            // var orderModels = orders!.Select(o => _mapper.Map<OrderViewModel>(o));

            var orderModels = orders!.Select(e => new OrderViewModel{
                OrderNumber = e.OrderNumber,
                OrderDate = e.OrderDate,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Address = e.Address,
                Email = e.Email,
                OrderState = e.OrderState,
                PaymentType = e.PaymentType,
                OrderItems = e.OrderItems!.Select(i => new OrderItemViewModel{
                    Price = i.Price,
                    Quantity = i.Quantity,
                    Name = i.Product!.Name,
                    ImageUrl = i.Product.ImageUrl,
                    Url = i.Product.Url,
                    Weight = i.Product.Weight
                }).ToList()
            });

            var ordersCount = await _unitOfWork.Orders.GetOrdersCount(userId);

            return new OrderListViewModel
            {
                Orders = orderModels.ToList(),
                PageInfo = new PageInfoModel {
                    TotalItems = ordersCount,
                    ItemPerPage = pageSize,
                    CurrentPage = page,
                    PaginationType = ""
                }
            };
        }
    }
}