using SimpleShoppingCart.DataAccess.Models;
using SimpleShoppingCart.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SimpleShoppingCart.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(ILogger<UserController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public bool Login(UserLoginViewModel userLogin)
        {
            var result = _signInManager.PasswordSignInAsync(userLogin.UserName, userLogin.Password, false, false).Result;

            return result.Succeeded;
        }
    }
}