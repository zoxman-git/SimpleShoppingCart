namespace SimpleShoppingCart.DataAccess.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public decimal Price { get; set; }

        public int? VolumeDiscountQuantity { get; set; }

        public decimal? VolumeDiscountPrice { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
