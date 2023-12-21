using Shared.Models;

namespace Presentation.Identity.Abstract
{
    public interface IUserService
    {
        string? Message { get; set; }
        Task<bool> Register(RegisterModel model);
    }
}