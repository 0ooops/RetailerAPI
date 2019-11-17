namespace RetailerAPI.Models
{
    /// <summary>
    /// UserToken is a model for user token information.
    /// </summary>
    public class UserToken : IUserToken
    {
        /// <summary>
        /// Name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// An identifier of the user for accessing the service.
        /// </summary>
        public string Token { get; set; }
    }
}
