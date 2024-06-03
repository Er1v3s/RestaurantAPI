using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace RestaurantAPI.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _contextAccessor.HttpContext?.User;
        public int? GetUserId => User is null ? null : (int?)int.Parse(User.FindFirst(user => user.Type == ClaimTypes.NameIdentifier).Value);
    }
}
