using SimpleShoppingCart.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleShoppingCart.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using (var context = new ApplicationDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Roles.Add(new IdentityRole()
                    {
                        Id = "b59940d1-3fcb-4696-9b4d-b5f5bd164406",
                        Name = "WebAppUser",
                        NormalizedName = "WebAppUser"
                    });

                    var hasher = new PasswordHasher<ApplicationUser>();

                    context.Users.Add(new ApplicationUser()
                    {
                        Id = "321b7d2f-2df7-4d9b-b049-434575aa1cba",
                        UserName = "sam_smith",
                        NormalizedUserName = "SAM_SMITH",
                        PasswordHash = hasher.HashPassword(null, "PASSword!234")
                    });

                    context.UserRoles.Add(new IdentityUserRole<string>()
                    {
                        UserId = "321b7d2f-2df7-4d9b-b049-434575aa1cba",
                        RoleId = "b59940d1-3fcb-4696-9b4d-b5f5bd164406"
                    });

                    context.Database.ExecuteSql($"SET IDENTITY_INSERT Products ON");

                    context.Products.Add(new Product()
                    {
                        Id = 1,
                        Code = "A",
                        Name = "Product A",
                        Price = 2.00m,
                        VolumeDiscountPrice = 1.75m,
                        VolumeDiscountQuantity = 4,
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true,
                        IsDeleted = false
                    });

                    context.Products.Add(new Product()
                    {
                        Id = 2,
                        Code = "B",
                        Name = "Product B",
                        Price = 12.00m,
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true,
                        IsDeleted = false
                    });

                    context.Products.Add(new Product()
                    {
                        Id = 3,
                        Code = "C",
                        Name = "Product C",
                        Price = 1.25m,
                        VolumeDiscountPrice = 1.00m,
                        VolumeDiscountQuantity = 6,
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true,
                        IsDeleted = false
                    });

                    context.Products.Add(new Product()
                    {
                        Id = 4,
                        Code = "D",
                        Name = "Product D",
                        Price = 0.15m,
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true,
                        IsDeleted = false
                    });

                    context.SaveChanges();

                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Products OFF");

                    transaction.Commit();
                }
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
