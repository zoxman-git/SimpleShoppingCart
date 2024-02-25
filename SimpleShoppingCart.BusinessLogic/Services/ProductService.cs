using SimpleShoppingCart.BusinessLogic.ViewModels;
using SimpleShoppingCart.DataAccess.Repositories;
using SimpleShoppingCart.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace SimpleShoppingCart.BusinessLogic.Services.Interfaces
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly IProductRepository _productRepository;

        public ProductService(ILogger<ProductRepository> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public IQueryable<ProductViewModel> GetProductList()
        {
            return _productRepository.GetProductList().Select(x => new ProductViewModel
            {

                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Price = x.Price,
                VolumeDiscountPrice = x.VolumeDiscountPrice,
                VolumeDiscountQuantity = x.VolumeDiscountQuantity
            });
        }
    }
}
