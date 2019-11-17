using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RetailerAPI.Models;

namespace RetailerAPI.Controllers
{
    /// <summary>
    /// UserController handles all requests related to user information.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// UserToken as options.
        /// </summary>
        private readonly IOptions<UserToken> userTokenOption;

        /// <summary>
        /// Initialize user controller and inject token option.
        /// </summary>
        /// <param name="userTokenOption">IOptions of UserToken type.</param>
        public UserController(IOptions<UserToken> userTokenOption)
        {
            this.userTokenOption = userTokenOption;
        }

        /// <summary>
        /// Get /user will return user name and token as a JSON object.
        /// </summary>
        /// <returns>Objects that fulfill IUserToken interface.</returns>
        [HttpGet]
        public ActionResult<IUserToken> Get()
        {
            return this.userTokenOption.Value;
        }
    }
}
