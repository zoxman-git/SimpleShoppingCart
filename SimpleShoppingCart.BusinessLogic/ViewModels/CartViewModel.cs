namespace SimpleShoppingCart.Web.Models
{
    public class CartViewModel
    {
        public CartViewModel() 
        {
            CartItems = new CartItem[0];
        }

        public CartItem[] CartItems { get; set; }
    }

    public class CartItem
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public decimal Price { get; set; }

        public int? VolumeDiscountQuantity { get; set; }

        public decimal? VolumeDiscountPrice { get; set; }
    }
}