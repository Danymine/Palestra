using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public static class AuthenticationService
    {
        public static async Task<string?> TakeIdByAuthenticationUserAsync(ClaimsPrincipal User, UserManager<AspNetUser> _userManager)
        {

            if (User?.Identity?.IsAuthenticated != true || string.IsNullOrEmpty(User.Identity.Name))
            {
                return null;
            }

            var result = await _userManager.FindByNameAsync(User.Identity.Name);
            if (result != null)
            {
                return result.Id;
            }

            return null;
        }
    }
}
