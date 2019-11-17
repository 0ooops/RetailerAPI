using System.Collections.Generic;

namespace RetailerAPI.Models
{
    /// <summary>
    /// Model for shopper history.
    /// </summary>
    public class ShopperHistory<T> : IShopperHistory<T> where T : IProduct
    {
        /// <summary>
        /// Unique identifier of customer.
        /// </summary>
        public string CustomerId { get; }

        /// <summary>
        /// List of products that a customer has purchased.
        /// </summary>
        public List<T> Products { get; set; }
    }
}
