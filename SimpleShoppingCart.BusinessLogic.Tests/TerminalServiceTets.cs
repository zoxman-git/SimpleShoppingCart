using SimpleShoppingCart.BusinessLogic.Services.Interfaces;
using SimpleShoppingCart.BusinessLogic.ViewModels;
using SimpleShoppingCart.Web.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace SimpleShoppingCart.BusinessLogic.Tests
{
    [TestClass]
    public class TerminalServiceTets
    {
        private readonly List<ProductViewModel> _productList = new List<ProductViewModel>()
        {
            new ProductViewModel() { Id = 1, Name = "Product A", Code = "A", Price = 2.00m, VolumeDiscountQuantity = 4, VolumeDiscountPrice = 1.75m },
            new ProductViewModel() { Id = 2, Name = "Product B", Code = "B", Price = 12.00m },
            new ProductViewModel() { Id = 3, Name = "Product C", Code = "C", Price = 1.25m, VolumeDiscountQuantity = 6, VolumeDiscountPrice = 1.00m },
            new ProductViewModel() { Id = 4, Name = "Product D", Code = "D", Price = 0.15m }
        };

        [TestMethod]
        public void Scan_FirstItemForProduct_SetQuantityOne()
        {
            var loggerMock = new Mock<ILogger<TerminalService>>();
            var productServiceMock = new Mock<IProductService>();

            var cartItemProductCodeToScan = "C";

            var terminalService = new TerminalService(loggerMock.Object, productServiceMock.Object);

            terminalService.Scan(cartItemProductCodeToScan);

            Assert.AreEqual(1, terminalService.DistinctProducts.Count);
            Assert.AreEqual("C", terminalService.DistinctProducts.First().Key);
            Assert.AreEqual("C", terminalService.DistinctProducts.First().Value.Code);
            Assert.AreEqual(1, terminalService.DistinctProducts.First().Value.Quantity);
            Assert.AreEqual(0, terminalService.DistinctProducts.First().Value.Total);
        }

        [TestMethod]
        public void Scan_TwoItemsForSameProduct_UpdateQuantityTwo()
        {
            var loggerMock = new Mock<ILogger<TerminalService>>();
            var productServiceMock = new Mock<IProductService>();

            var cartItemProductCodeToScan = "C";

            var terminalService = new TerminalService(loggerMock.Object, productServiceMock.Object);

            terminalService.Scan(cartItemProductCodeToScan);
            terminalService.Scan(cartItemProductCodeToScan);

            Assert.AreEqual(1, terminalService.DistinctProducts.Count);
            Assert.AreEqual("C", terminalService.DistinctProducts.First().Key);
            Assert.AreEqual("C", terminalService.DistinctProducts["C"].Code);
            Assert.AreEqual(2, terminalService.DistinctProducts["C"].Quantity);
            Assert.AreEqual(0, terminalService.DistinctProducts["C"].Total);
        }

        [TestMethod]
        public void Scan_NullCode_ThrowException()
        {
            var loggerMock = new Mock<ILogger<TerminalService>>();
            var productServiceMock = new Mock<IProductService>();

            var terminalService = new TerminalService(loggerMock.Object, productServiceMock.Object);

            var ex = Assert.ThrowsException<ArgumentException>(() => terminalService.Scan(null));

            Assert.AreEqual("Invalid input", ex.Message);
        }

        [TestMethod]
        public void Scan_EmptyCode_ThrowException()
        {
            var loggerMock = new Mock<ILogger<TerminalService>>();
            var productServiceMock = new Mock<IProductService>();

            var terminalService = new TerminalService(loggerMock.Object, productServiceMock.Object);

            var ex = Assert.ThrowsException<ArgumentException>(() => terminalService.Scan(string.Empty));

            Assert.AreEqual("Invalid input", ex.Message);
        }

        [TestMethod]
        public void Total_NoItemsScanned_ReturnTotalZero()
        {
            var loggerMock = new Mock<ILogger<TerminalService>>();
            var productServiceMock = new Mock<IProductService>();

            productServiceMock.Setup(x => x.GetProductList()).Returns(_productList.AsQueryable());

            var terminalService = new TerminalService(loggerMock.Object, productServiceMock.Object);

            var total = terminalService.Total();

            Assert.AreEqual(0m, total);
        }

        [TestMethod]
        public void Total_ScanABCDABAA_ReturnTotalThirtyTwoDollarsFortyCents()
        {
            var loggerMock = new Mock<ILogger<TerminalService>>();
            var productServiceMock = new Mock<IProductService>();

            productServiceMock.Setup(x => x.GetProductList()).Returns(_productList.AsQueryable());

            var terminalService = new TerminalService(loggerMock.Object, productServiceMock.Object);

            terminalService.Scan("A");
            terminalService.Scan("B");
            terminalService.Scan("C");

            terminalService.Scan("D");
            terminalService.Scan("A");
            terminalService.Scan("B");

            terminalService.Scan("A");
            terminalService.Scan("A");

            var total = terminalService.Total();

            Assert.AreEqual(32.40m, total);
        }

        [TestMethod]
        public void Total_ScanCCCCCCC_ReturnTotalSevenDollarsTwentyFiveCents()
        {
            var loggerMock = new Mock<ILogger<TerminalService>>();
            var productServiceMock = new Mock<IProductService>();

            productServiceMock.Setup(x => x.GetProductList()).Returns(_productList.AsQueryable());

            var terminalService = new TerminalService(loggerMock.Object, productServiceMock.Object);

            terminalService.Scan("C");
            terminalService.Scan("C");
            terminalService.Scan("C");

            terminalService.Scan("C");
            terminalService.Scan("C");
            terminalService.Scan("C");

            terminalService.Scan("C");

            var total = terminalService.Total();

            Assert.AreEqual(7.25m, total);
        }

        [TestMethod]
        public void Total_ScanABCD_ReturnTotalFifteenDollarsFortyCents()
        {
            var loggerMock = new Mock<ILogger<TerminalService>>();
            var productServiceMock = new Mock<IProductService>();

            productServiceMock.Setup(x => x.GetProductList()).Returns(_productList.AsQueryable());

            var terminalService = new TerminalService(loggerMock.Object, productServiceMock.Object);

            terminalService.Scan("A");
            terminalService.Scan("B");
            terminalService.Scan("C");

            terminalService.Scan("D");

            var total = terminalService.Total();

            Assert.AreEqual(15.40m, total);
        }

        [TestMethod]
        public void TotalWithCart_EmptyCart_ReturnTotalZero()
        {
            var loggerMock = new Mock<ILogger<TerminalService>>();
            var productServiceMock = new Mock<IProductService>();

            productServiceMock.Setup(x => x.GetProductList()).Returns(_productList.AsQueryable());

            var cart = new CartViewModel();

            var terminalService = new TerminalService(loggerMock.Object, productServiceMock.Object);

            var total = terminalService.Total(cart);

            Assert.AreEqual(0m, total);
        }

        [TestMethod]
        public void TotalWithCart_ABCDABAA_ReturnTotalThirtyTwoDollarsFortyCents()
        {
            var loggerMock = new Mock<ILogger<TerminalService>>();
            var productServiceMock = new Mock<IProductService>();

            productServiceMock.Setup(x => x.GetProductList()).Returns(_productList.AsQueryable());

            var cart = new CartViewModel()
            {
                CartItems = new CartItem[] { 
                    new CartItem { Code = "A" },
                    new CartItem { Code = "B" },
                    new CartItem { Code = "C" },
                    new CartItem { Code = "D" },
                    new CartItem { Code = "A" },
                    new CartItem { Code = "B" },
                    new CartItem { Code = "A" },
                    new CartItem { Code = "A" }
                }
            };

            var terminalService = new TerminalService(loggerMock.Object, productServiceMock.Object);

            var total = terminalService.Total(cart);

            Assert.AreEqual(32.40m, total);
        }
    }
}