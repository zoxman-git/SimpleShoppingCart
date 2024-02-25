using SimpleShoppingCart.DataAccess.Models;

namespace SimpleShoppingCart.DataAccess.Repositories.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> GetProductList();
    }
}
