using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Shared.Models;

namespace Presentation.PaymentProcess
{
    public static class Process_Iyzipay
    {
        public static Payment Pay(OrderModel model,IConfiguration configuration)
        {
            Options options = new()
            {
                ApiKey = configuration["Payment:Iyzipay:ApiKey"],
                SecretKey = configuration["Payment:Iyzipay:SecretKey"],
                BaseUrl = configuration["Payment:Iyzipay:BaseUrl"]
            };


            CreatePaymentRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = new Random().Next(111111111, 999999999).ToString(),
                Price = model.CartModel!.TotalPrice().ToString(),
                PaidPrice = model.CartModel.TotalPrice().ToString(),
                Currency = Currency.TRY.ToString(),
                Installment = 1,
                BasketId = "B67832",
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString()
            };

            PaymentCard paymentCard = new()
            {
                CardHolderName = model.CardName,
                CardNumber = model.CardNumber,
                ExpireMonth = model.ExpirationMonth,
                ExpireYear = model.ExpirationYear,
                Cvc = model.Cvc,
                RegisterCard = 0
            };

            request.PaymentCard = paymentCard;

            Buyer buyer = new()
            {
                Id = "BY789",
                Name = model.FirstName,
                Surname = model.LastName,
                GsmNumber = model.Phone.ToString(),
                Email = model.Email,
                IdentityNumber = "74300864791",
                LastLoginDate = DateTime.Now.ToString(),
                RegistrationDate = "2013-04-21 15:12:09",
                RegistrationAddress = model.Address,
                Ip = "85.34.78.112",
                City = model.City,
                Country = "Turkey",
                ZipCode = "34732"
            };
            request.Buyer = buyer;

            Address shippingAddress = new()
            {
                ContactName = model.FirstName,
                City = model.City,
                Country = "Turkey",
                Description = model.Address,
                ZipCode = "34742"
            };
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new()
            {
                ContactName = model.FirstName,
                City = model.City,
                Country = "Turkey",
                Description = model.Address,
                ZipCode = "34742"
            };
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = [];
            BasketItem basketItem = new();

            foreach (var item in model.CartModel.CartItems!)
            {
                basketItem = new BasketItem
                {
                    Id = item.ProductId.ToString(),
                    Name = item.Name,
                    Category1 = "Yöresel ürün",
                    Price = item.TotalPrice().ToString(),
                    ItemType = BasketItemType.PHYSICAL.ToString()
                };

                basketItems.Add(basketItem);
            }

            request.BasketItems = basketItems;

            return Payment.Create(request, options);
        }
      
    }      
}

