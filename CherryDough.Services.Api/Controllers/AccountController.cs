using System.Threading.Tasks;
using CherryDough.Infra.Identity;
using CherryDough.Infra.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CherryDough.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private JwtSettings _jwtSettings;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<JwtSettings> jwtSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterUser registerUser)
        {
            if (!ModelState.IsValid) return ShowcaseResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                return ShowcaseResponse(GetFullJwt(user.Email));
            }

            foreach (var error in result.Errors)
            {
                AddError(error.Description);
            }

            return ShowcaseResponse();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            if (!ModelState.IsValid) return ShowcaseResponse(ModelState);

            var result = await _signInManager
                .PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                var fullJwt = GetFullJwt(loginUser.Email);
                return ShowcaseResponse(fullJwt);
            }

            if (result.IsLockedOut)
            {
                AddError("The user has been locked temporarily");
                return ShowcaseResponse();
            }
            
            AddError("Incorrect user or password");
            return ShowcaseResponse();
        }

        private string GetFullJwt(string email)
        {
            return new JwtBuilder()
                .WithUserManager(_userManager)
                .WithJwtSettings(_jwtSettings)
                .WithEmail(email)
                .WithJwtClaims()
                .WithUserClaims()
                .WithUserRoles()
                .BuildToken();
        }
    }
}