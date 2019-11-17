using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RetailerAPI.Services;

namespace RetailerAPI.Controllers
{
    /// <summary>
    /// TrolleyTotalController handles all requests related to
    /// trolley total calculation.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TrolleyTotalController : ControllerBase
    {
        private readonly IResourceService resourceService;
        private const string TrolleySchemaStr = @"
            {
                'products': [
                {
                    'name': 'string',
                    'price': 0
                }
                ],
                'specials': [
                {
                    'quantities': [
                    {
                        'name': 'string',
                        'quantity': 0
                    }
                    ],
                    'total': 0
                }
                ],
                'quantities': [
                {
                    'name': 'string',
                    'quantity': 0
                }
                ]
            }";

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
        /// <param name="trolley">
        /// A JObject contains list of products, specials, quantities.</param>
        /// <returns>A lowest possible total price.</returns>
        [HttpPost]
        public ActionResult<double> Post(JObject trolley)
        {
            
            JsonSchema schema = JsonSchema.Parse(TrolleySchemaStr);

            // Check schema.
            if (!trolley.IsValid(schema, out IList<string> messages))
            {
                string errorMessage = string.Join(": ", messages.ToArray());
                throw new ArgumentException(errorMessage);
            }

            return this.resourceService.GetMinimumTotal(trolley);
        }
    }
}
