namespace RetailerAPI.Models
{
    /// <summary>
    /// Interface for products in general.
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// Name of the product.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Price of the product.
        /// </summary>
        double Price { get; set; }

        /// <summary>
        /// Quantity of the product.
        /// </summary>
        double Quantity { get; set; }
    }
}
