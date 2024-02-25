using SimpleShoppingCart.Web.Models;
using Microsoft.Extensions.Logging;

namespace SimpleShoppingCart.BusinessLogic.Services.Interfaces
{
    public class TerminalService : ITerminalService
    {
        private readonly ILogger<TerminalService> _logger;
        private readonly IProductService _productService;
        private Dictionary<string, DistinctProduct> _distinctProducts = new Dictionary<string, DistinctProduct>();
        
        public Dictionary<string, DistinctProduct> DistinctProducts => _distinctProducts;

        public TerminalService(ILogger<TerminalService> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public void Scan(string item)
        {
            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentException("Invalid input");

            if (_distinctProducts.ContainsKey(item))
                _distinctProducts[item].Quantity++;
            else
                _distinctProducts.Add(item, new DistinctProduct() { Code = item, Quantity = 1 });
        }
        
        public decimal Total()
        {
            var total = 0m;
            var products = _productService.GetProductList().ToDictionary(x => x.Code, x => x);

            foreach (var item in _distinctProducts)
            {
                if (products[item.Key].VolumeDiscountQuantity != null && item.Value.Quantity >= products[item.Key].VolumeDiscountQuantity.Value)
                {
                    // Make it explicit we are rounding down, rather than just dividing int by int and getting int
                    var dicountedQuantityBatches = (decimal)item.Value.Quantity / (decimal)products[item.Key].VolumeDiscountQuantity.Value;
                    var dicountedQuantity = Math.Floor(dicountedQuantityBatches) * products[item.Key].VolumeDiscountQuantity.Value;

                    item.Value.Total = dicountedQuantity * products[item.Key].VolumeDiscountPrice.Value;

                    var fullPriceQuantity = item.Value.Quantity - dicountedQuantity;

                    item.Value.Total += fullPriceQuantity * products[item.Key].Price;
                }
                else
                {
                    item.Value.Total = item.Value.Quantity * products[item.Key].Price;
                }

                total += item.Value.Total;
            }

            return total;
        }

        public decimal Total(CartViewModel cart)
        {
            foreach (var item in cart.CartItems)
            {
                Scan(item.Code);    
            }

            return Total();
        }

        public class DistinctProduct
        {
            public string Code { get; set; }
            
            public int Quantity { get; set; }

            public decimal Total { get; set; }
        }
    }
}
