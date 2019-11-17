using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailerAPI.Models
{
    /// <summary>
    /// IUserToken acts as an interface for communications related to user token.
    /// </summary>
    public interface IUserToken
    {
        /// <summary>
        /// Name of the user.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// An identifier of the user for accessing the service.
        /// </summary>
        string Token { get; }
    }
}
