using System.Collections.Generic;
using System.Linq;
using RetailerAPI.Models;

namespace RetailerAPI.Services
{
    public class SortService : ISortService
    {
        /// <summary>
        /// Sort list of products by price.
        /// </summary>
        /// <param name="products">List of products.</param>
        /// <param name="isAscending">Sorting direction.</param>
        /// <returns>Sorted products in list.</returns>
        public List<IProduct> SortProductsByPrice(
            List<IProduct> products,
            bool isAscending)
        {
            return isAscending ?
                products.OrderBy(p => p.Price).ToList()
                : products.OrderByDescending(p => p.Price).ToList();
        }

        /// <summary>
        /// Sort list of products alphabetically by name.
        /// </summary>
        /// <param name="products">List of products.</param>
        /// <param name="isAscending">Sorting direction.</param>
        /// <returns>Sorted products in list.</returns>
        public List<IProduct> SortProductsByName(
            List<IProduct> products,
            bool isAscending)
        {
            return isAscending ?
                products.OrderBy(p => p.Name).ToList()
                : products.OrderByDescending(p => p.Name).ToList();
        }

        /// <summary>
        /// Sort list of products by popularity.
        /// </summary>
        /// <param name="products">List of products.</param>
        /// <param name="popDict">
        /// Dictionary with product name as key andquantity/popularity as value.
        /// </param>
        /// <returns>Sorted products in list.</returns>
        public List<IProduct> SortProductsByPopularity(
            List<IProduct> products,
            Dictionary<string, double> popDict)
        {
            Dictionary<IProduct, double> prodDict = new Dictionary<IProduct, double>();

            foreach (IProduct product in products)
            {
                // If the popDict doesn't contain the product name,
                // use -1.0 as default popularity.
                double popularity = popDict.ContainsKey(product.Name) ? 
                    popDict[product.Name] : -1.0;
                prodDict[product] = popularity;
            }

            // Sort products by popularity
            return prodDict
                .OrderByDescending(item => item.Value)
                .Select(item => item.Key)
                .ToList();
        }

        /// <summary>
        /// Calculate product sales quantity.
        /// </summary>
        /// <param name="shopperHistories">List of shopper histories.</param>
        /// <returns>
        /// Dictionary with product name as key and quantity/popularity as value.
        /// </returns>
        public Dictionary<string, double> CalculatePopularityFromShopperHistory(
            List<IShopperHistory<IProduct>> shopperHistories)
        {
            Dictionary<string, double> productQuantity = new Dictionary<string, double>();

            // Count selling quantity for each product
            foreach (IShopperHistory<IProduct> shopperHistory in shopperHistories)
            {
                foreach (IProduct product in shopperHistory.Products)
                {
                    if (productQuantity.ContainsKey(product.Name))
                    {
                        productQuantity[product.Name] += product.Quantity;
                    }
                    else
                    {
                        productQuantity[product.Name] = product.Quantity;
                    }
                }
            }

            return productQuantity;
        }
    }
}
