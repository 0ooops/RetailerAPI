using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RetailerAPI.Models;
using RetailerAPI.Services;

namespace RetailerAPI.Controllers
{
    /// TODO: Write unit tests for this controller
    /// <summary>
    /// SortController will be used to handle requests related to sort
    /// products in different way.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SortController : Controller
    {
        private readonly ISortService sortService;
        private readonly IResourceService resourceService;

        public SortController(
            ISortService sortService,
            IResourceService resourceService)
        {
            this.sortService = sortService;
            this.resourceService = resourceService;
        }

        /// <summary>
        /// Get /sort will return sorted list of products.
        /// </summary>
        /// <returns>List of products.</returns>
        [HttpGet]
        public ActionResult<List<IProduct>> Get(string sortOption)
        {
            List<IProduct> afterSortProducts = new List<IProduct>();

            // Get products to be sorted
            List<IProduct> preSortProducts = this.resourceService.GetProducts();

            /// TODO: Move hardcoded constants to a separate class
            // Map sortOption to sorting methods
            if (sortOption.Equals("Low", StringComparison.InvariantCultureIgnoreCase))
            {
                afterSortProducts = 
                    this.sortService.SortProductsByPrice(preSortProducts, isAscending : true);
            }
            else if (sortOption.Equals("High", StringComparison.InvariantCultureIgnoreCase))
            {
                afterSortProducts =
                    this.sortService.SortProductsByPrice(preSortProducts, isAscending: false);
            }
            else if (sortOption.Equals("Ascending", StringComparison.InvariantCultureIgnoreCase))
            {
                afterSortProducts =
                    this.sortService.SortProductsByName(preSortProducts, isAscending: true);
            }
            else if (sortOption.Equals("Descending", StringComparison.InvariantCultureIgnoreCase))
            {
                afterSortProducts =
                    this.sortService.SortProductsByName(preSortProducts, isAscending: false);
            }
            else if (sortOption.Equals("Recommended", StringComparison.InvariantCultureIgnoreCase))
            {
                List<IShopperHistory<IProduct>> shopperHistory = this.resourceService.GetShopperHistories();

                // Use shopperHistory to generate popularity dict
                Dictionary<string, double> popDict = this.sortService.CalculatePopularityFromShopperHistory(shopperHistory);
                afterSortProducts =
                    this.sortService.SortProductsByPopularity(preSortProducts, popDict);
            }

            return afterSortProducts;
        }
    }
}