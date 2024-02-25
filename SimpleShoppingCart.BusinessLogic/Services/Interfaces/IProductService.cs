using SimpleShoppingCart.BusinessLogic.ViewModels;

namespace SimpleShoppingCart.BusinessLogic.Services.Interfaces
{
    public interface IProductService
    {
        IQueryable<ProductViewModel> GetProductList();
    }
}
