namespace SimpleShoppingCart.BusinessLogic.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public decimal Price { get; set; }

        public int? VolumeDiscountQuantity { get; set; }

        public decimal? VolumeDiscountPrice { get; set; }
    }
}
