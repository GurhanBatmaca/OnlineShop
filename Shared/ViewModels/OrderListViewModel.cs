using Shared.ViewModels;

namespace Shared.Models
{
    public class OrderListViewModel
    {       
        public List<OrderViewModel>? Orders { get; set; }
        public PageInfoModel? PageInfo { get; set; }
    }
}