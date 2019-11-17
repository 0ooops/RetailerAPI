using System.Collections.Generic;

namespace RetailerAPI.Models
{
    /// <summary>
    /// Interface for shopper history.
    /// </summary>
    public interface IShopperHistory<T>
    {
        /// <summary>
        /// Unique identifier of customer.
        /// </summary>
        string CustomerId { get; }

        /// <summary>
        /// List of products that a customer has purchased.
        /// </summary>
        List<T> Products { get; set; }
    }
}
