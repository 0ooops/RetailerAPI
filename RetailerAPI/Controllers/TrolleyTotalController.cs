using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RetailerAPI.Models;
using RetailerAPI.Services;

namespace RetailerAPI.Controllers
{
    /// <summary>
    /// TrolleyTotalController handles all requests related to trolley total calculation.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TrolleyTotalController : ControllerBase
    {
        private readonly IResourceService resourceService;

        /// <summary>
        /// Initialize TrolleyTotal controller and inject resource service.
        /// </summary>
        /// <param name="resourceService">
        /// Resource service that helps handle dependent resource logics.
        /// </param>
        public TrolleyTotalController(IResourceService resourceService)
        {
            this.resourceService = resourceService;
        }

        /// <summary>
        /// Post /TrolleyTotal will return minimum price to purchase the products.
        /// </summary>
        /// <param name="trolley">A JObject contains list of products, specials, quantities.</param>
        /// <returns>A lowest possible total price.</returns>
        [HttpPost]
        public ActionResult<double> Post(JObject trolley)
        {
            if (!IsValidTrolleyJson(trolley))
            {
                throw new ArgumentException(
                    "Required keys products, specials, quantities are not all presented.");
            }
            return this.resourceService.GetMinimumTotal(trolley);
        }

        /// <summary>
        /// Quick validation to check if trolley object is of right structure.
        /// </summary>
        /// <param name="trolley">JObject represents a trolley.</param>
        /// <returns>If the JObject is of right structure.</returns>
        private bool IsValidTrolleyJson(JObject trolley)
        {
            if (!trolley.ContainsKey("products")
                || !trolley.ContainsKey("specials")
                || !trolley.ContainsKey("quantities"))
            {
                return false;
            }
            return true;
        }
    }
}
