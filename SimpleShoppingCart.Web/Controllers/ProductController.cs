using SimpleShoppingCart.BusinessLogic.Services.Interfaces;
using SimpleShoppingCart.BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace SimpleShoppingCart.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public List<ProductViewModel> Get()
        {
            return _productService.GetProductList().ToList();
        }
    }
}