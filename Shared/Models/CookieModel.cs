using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Shared.Models
{
    public class CookieModel
    {
        public CookieOptions? CookieOptions { get; set; } = new CookieOptions{Expires = DateTime.Now.AddYears(1)};
        public HttpContext? HttpContext { get; set; }

    }
}