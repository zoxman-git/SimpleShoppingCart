using SimpleShoppingCart.BusinessLogic.Services.Interfaces;
using SimpleShoppingCart.DataAccess.Models;
using SimpleShoppingCart.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SimpleShoppingCart.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly ITerminalService _terminalService;

        public CartController(ILogger<CartController> logger, ITerminalService terminalService)
        {
            _logger = logger;
            _terminalService = terminalService;
        }

        [HttpPost("calculateTotal")]
        public decimal CalculateTotal(CartViewModel cart)
        {
            var cartTotal = _terminalService.Total(cart);

            return cartTotal;
        }
    }
}