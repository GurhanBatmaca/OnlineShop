using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shared.ViewModels
{
    public class OrderBriefViewModel
    {
        public string? OrderStateName { get; set; }
        public int Count { get; set; }

    }
}