using SimpleShoppingCart.DataAccess.Models;
using SimpleShoppingCart.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace SimpleShoppingCart.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductRepository(ILogger<ProductRepository> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<Product> GetProductList()
        {
            var products =
                from product in _applicationDbContext.Products
                select product;

            return products;
        }
    }
}
