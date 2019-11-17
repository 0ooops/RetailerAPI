using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RetailerAPI.Models;

namespace RetailerAPI.Services
{
    /// <summary>
    /// Interface for getting all resources this project has dependency on.
    /// </summary>
    public interface IResourceService
    {
        /// <summary>
        /// Get list of products from dependent services.
        /// </summary>
        /// <returns>List of products.</returns>
        List<IProduct> GetProducts();

        /// <summary>
        /// Get list of shopper history from dependent services.
        /// </summary>
        /// <returns>List of shopper histories.</returns>
        List<IShopperHistory<IProduct>> GetShopperHistories();

        /// <summary>
        /// Call API to calculate the minimum total price of list of products.
        /// </summary>
        /// <param name="trolley">Trolley with list of products and specials.</param>
        /// <returns>Minimum total price in double.</returns>
        double GetMinimumTotal(JObject trolley);
    }
}
