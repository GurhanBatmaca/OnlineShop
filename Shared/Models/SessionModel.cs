using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Shared.Models
{
    public class SessionModel
    {
        public HttpContext? HttpContext { get; set; }  

    }
}