using System.Collections.Generic;
using RetailerAPI.Models;

namespace RetailerAPI.Services
{
    public interface ISortService
    {
        /// <summary>
        /// Sort list of products by price.
        /// </summary>
        /// <param name="products">List of products.</param>
        /// <param name="isAscending">Sorting direction.</param>
        /// <returns>Sorted products in list.</returns>
        List<IProduct> SortProductsByPrice(List<IProduct> products, bool isAscending);

        /// <summary>
        /// Sort list of products alphabetically by name.
        /// </summary>
        /// <param name="products">List of products.</param>
        /// <param name="isAscending">Sorting direction.</param>
        /// <returns>Sorted products in list.</returns>
        List<IProduct> SortProductsByName(List<IProduct> products, bool isAscending);

        /// <summary>
        /// Sort list of products by popularity.
        /// </summary>
        /// <param name="products">List of products.</param>
        /// <param name="popDict">Dictionary with product name as key and quantity/popularity as value.</param>
        /// <returns>Sorted products in list.</returns>
        List<IProduct> SortProductsByPopularity(List<IProduct> products, Dictionary<string, double> popDict);

        /// <summary>
        /// Calculate product sales quantity.
        /// </summary>
        /// <param name="shopperHistories">List of shopper histories.</param>
        /// <returns>Dictionary with product name as key and quantity/popularity as value.</returns>
        Dictionary<string, double> CalculatePopularityFromShopperHistory(List<IShopperHistory<IProduct>> shopperHistories);
    }
}
