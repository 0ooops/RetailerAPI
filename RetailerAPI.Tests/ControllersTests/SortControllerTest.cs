using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RetailerAPI.Models;
using RetailerAPI.Services;

namespace RetailerAPI.Controllers.Tests
{
    public class SortControllerTest
    {
        private readonly IProduct product1 = new Product
        {
            Name = "C Product",
            Price = 3.5,
            Quantity = 20
        };

        private readonly IProduct product2 = new Product
        {
            Name = "B Product",
            Price = 1.0,
            Quantity = 14
        };

        private readonly IProduct product3 = new Product
        {
            Name = "A Product",
            Price = 2.2,
            Quantity = 32
        };

        [Test]
        public void TestSortProductsByPrice()
        {
            // Expected result
            List<IProduct> products = new List<IProduct>
            {
                product1,
                product2,
                product3
            };

            // Set up behavior
            Mock<ISortService> sortService = new Mock<ISortService>();
            sortService.Setup(s => s.SortProductsByPrice(products, true)).Returns(products);
            Mock<IResourceService> resourceService = new Mock<IResourceService>();
            resourceService.Setup(r => r.GetProducts()).Returns(products);

            // Call method and assert
            SortController sortController =
                new SortController(sortService.Object, resourceService.Object);
            Assert.AreEqual(products, sortController.Get("Low").Value);    
        }
    }
}