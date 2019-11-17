namespace RetailerAPI.Models
{
    /// <summary>
    /// General type product.
    /// </summary>
    public class Product : IProduct
    {
        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Price of the product.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Quantity of the product.
        /// </summary>
        public double Quantity { get; set; }
    }
}
