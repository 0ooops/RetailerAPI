using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using RetailerAPI.Models;
using RetailerAPI.Services;
using System.Collections.Generic;

namespace RetailerAPI.Controllers.Tests
{
    public class SortServiceTest
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

        /// <summary>
        /// An unsorted product list that can be used for testing.
        /// </summary>
        private List<IProduct> beforeSorting;

        /// <summary>
        /// SortingService instance used for testing.
        /// </summary>
        private ISortService sortingService;

        [SetUp]
        public void SetUp()
        {
            this.beforeSorting = new List<IProduct>
            {
                product1,
                product2,
                product3
            };
            this.sortingService = new SortService();
        }

        [Test]
        public void TestSortProductsByPriceAscending()
        {
            // Expected result
            List<IProduct> afterSorting = new List<IProduct>
            {
                product2,
                product3,
                product1
            };

            // Call method
            List<IProduct> result = 
                this.sortingService.SortProductsByPrice(this.beforeSorting, true);

            // Assert
            Assert.AreEqual(afterSorting, result);
        }

        [Test]
        public void TestSortProductsByPriceDescending()
        {
            // Expected result
            List<IProduct> afterSorting = new List<IProduct>
            {
                product1,
                product3,
                product2
            };

            // Call method
            List<IProduct> result =
                this.sortingService.SortProductsByPrice(this.beforeSorting, false);

            // Assert
            Assert.AreEqual(afterSorting, result);
        }

        [Test]
        public void TestSortProductsByNameAscending()
        {
            // Expected result
            List<IProduct> afterSorting = new List<IProduct>
            {
                product3,
                product2,
                product1
            };

            // Call method
            List<IProduct> result =
                this.sortingService.SortProductsByName(this.beforeSorting, true);

            // Assert
            Assert.AreEqual(afterSorting, result);
        }

        [Test]
        public void TestSortProductsByNameDescending()
        {
            // Expected result
            List<IProduct> afterSorting = new List<IProduct>
            {
                product1,
                product2,
                product3
            };

            // Call method
            List<IProduct> result =
                this.sortingService.SortProductsByName(this.beforeSorting, false);

            // Assert
            Assert.AreEqual(afterSorting, result);
        }

        [Test]
        public void TestSortProductsByPopularity()
        {
            // Expected result
            List<IProduct> afterSorting = new List<IProduct>
            {
                product2,
                product3,
                product1
            };

            // Call method
            Dictionary<string, int> popDict = new Dictionary<string, int>
            {
                { product2.Name, 300 },
                { product3.Name, 200 },
                { product1.Name, 100 }
            };
            List<IProduct> result =
                this.sortingService.SortProductsByPopularity(this.beforeSorting, popDict);

            // Assert
            Assert.AreEqual(afterSorting, result);
        }

        [Test]
        public void TestCalculatePopularityFromShopperHistory()
        {
            // Input and expected output
            List<IShopperHistory> shopperHistories = new List<IShopperHistory>
            {
                new ShopperHistory { Products = this.beforeSorting },
                new ShopperHistory { Products = this.beforeSorting },
                new ShopperHistory { Products = this.beforeSorting }
            };
            Dictionary<string, int> popDict = new Dictionary<string, int>
            {
                { product1.Name, product1.Quantity * 3 },
                { product2.Name, product2.Quantity * 3 },
                { product3.Name, product3.Quantity * 3 },
            };

            // Call method
            Dictionary<string, int> result =
                this.sortingService.CalculatePopularityFromShopperHistory(shopperHistories);

            // Assert
            Assert.AreEqual(popDict, result);
        }
    }
}