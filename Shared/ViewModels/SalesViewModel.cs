using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shared.ViewModels
{
    public class SalesViewModel
    {
        public string? Name { get; set; }
        public int Sepet { get; set; }
        public double? SatisTotal { get; set; }
        public int? SatisAdet { get; set; }

    }
}