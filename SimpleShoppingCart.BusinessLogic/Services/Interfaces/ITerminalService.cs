using SimpleShoppingCart.Web.Models;

namespace SimpleShoppingCart.BusinessLogic.Services.Interfaces
{
    public interface ITerminalService
    {
        void Scan(string item);

        decimal Total();

        decimal Total(CartViewModel cart);
    }
}
