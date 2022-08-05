using Catalogo.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace Catalogo.Infrastructure.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticateService(UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = email,
                Email = email
            };
            var result = await _userManager.CreateAsync(applicationUser, password);
            return result.Succeeded;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password,
                isPersistent: false, lockoutOnFailure: false);

            return result.Succeeded;
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
