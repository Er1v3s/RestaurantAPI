using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Authorization
{
    public class CreatedMultipleRestaurantRequirementHandler : AuthorizationHandler<CreatedMultipleRestaurantRequirement>
    {
        private readonly RestaurantDbContext _context;

        public CreatedMultipleRestaurantRequirementHandler(RestaurantDbContext context)
        {
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantRequirement requirement)
        {
            var userId = int.Parse(context.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier).Value);

            var createdRestaurantsCount = _context
                .Restaurants
                .Count(r => r.CreatedById == userId);

            if(createdRestaurantsCount >= requirement.MinimumRestaurantCreated)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
