using Shared.Models;

namespace Presentation.Identity.Abstract
{
    public interface ISignService
    {
        string? Message { get; set; }
        Task<bool> Login(LoginModel model);
    }
}