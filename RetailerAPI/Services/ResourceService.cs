using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RetailerAPI.Models;

namespace RetailerAPI.Services
{
    /// <summary>
    /// Service for getting all resources this project has dependency on.
    /// </summary>
    public class ResourceService : IResourceService
    {
        private readonly HttpClient httpClient;
        private readonly IUserToken userToken;

        public ResourceService(
            IHttpClientFactory clientFactory,
            IOptions<UserToken> userTokenOption)
        {
            this.httpClient = clientFactory.CreateClient();
            this.userToken = userTokenOption.Value;
        }

        /// <summary>
        /// Get list of products from dependent services.
        /// </summary>
        /// <returns>List of products.</returns>
        /// <remarks>
        /// TODO: Research on another interesting way of handling deserializing error: 
        /// https://stackoverflow.com/questions/26264547/what-exceptions-does-newtonsoft-json-deserializeobject-throw
        /// </remarks>
        public List<IProduct> GetProducts()
        {
            string productUrl =
                "http://dev-wooliesx-recruitment.azurewebsites.net/api/resource/products";
            string responseContent =
                this.HttpGetHelper(this.GetUrlWithParamToken(productUrl)).Result;

            try
            {
                List<IProduct> products = 
                    JsonConvert.DeserializeObject<List<Product>>(responseContent)
                    .ConvertAll(p => (IProduct)p);
                return products;
            }
            catch (JsonSerializationException e) {
                /// TODO: log error
                throw e;
            }
        }

        /// <summary>
        /// Get list of shopper history from dependent services.
        /// </summary>
        /// <returns>List of shopper histories.</returns>
        public List<IShopperHistory<IProduct>> GetShopperHistories()
        {
            string shopperHistoryUrl =
                "http://dev-wooliesx-recruitment.azurewebsites.net/api/resource/shopperHistory";
            string responseContent =
                this.HttpGetHelper(this.GetUrlWithParamToken(shopperHistoryUrl)).Result;

            try
            {
                List<ShopperHistory<Product>> shopperHistories =
                    JsonConvert.DeserializeObject<List<ShopperHistory<Product>>>(responseContent);

                // Cast shopper histories from List<ShopperHistory<Product>> to List<IShopperHistory<IProduct>>
                List<IShopperHistory<IProduct>> newHistories =
                    new List<IShopperHistory<IProduct>>();
                foreach (ShopperHistory<Product> shopper in shopperHistories)
                {
                    List<IProduct> products = shopper.Products.ConvertAll(p => (IProduct)p);
                    newHistories.Add(new ShopperHistory<IProduct> { Products = products });
                }

                return newHistories;
            }
            catch (JsonSerializationException e)
            {
                /// TODO: log error
                throw e;
            }
        }

        /// <summary>
        /// Call API to calculate the minimum total price of list of products.
        /// </summary>
        /// <param name="trolley">Trolley with list of products and specials.</param>
        /// <returns>Minimum total price in double.</returns>
        public double GetMinimumTotal(JObject trolley)
        {
            string trolleyTotalUrl =
                "http://dev-wooliesx-recruitment.azurewebsites.net/api/resource/trolleyCalculator";

            // Encode trolley content as request body
            var byteContent = new ByteArrayContent(
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(trolley)));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            string responseContent = this.HttpPostHelper(
                this.GetUrlWithParamToken(trolleyTotalUrl), byteContent).Result;

            double result;
            if (double.TryParse(responseContent, out result))
            {
                return result;
            }

            return 0.0;
        }

        /// <summary>
        /// Helper method to get request url with token as parameter.
        /// </summary>
        /// <param name="baseUrl">Base url of request.</param>
        /// <returns>Url with token as parameter.</returns>
        private string GetUrlWithParamToken(string baseUrl)
        {
            UriBuilder urlBuilder = new UriBuilder(baseUrl);
            var query = HttpUtility.ParseQueryString(urlBuilder.Query);

            // Add token as parameter
            query["token"] = this.userToken.Token;
            urlBuilder.Query = query.ToString();

            return urlBuilder.ToString();
        }

        /// <summary>
        /// Helper method used to send http get request.
        /// </summary>
        /// <returns>Response string.</returns>
        private async Task<string> HttpGetHelper(string url)
        {
            try
            {
                using (HttpResponseMessage httpResponse = 
                    await this.httpClient.GetAsync(url).ConfigureAwait(false))
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        return await httpResponse.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (WebException e)
            {
                // TODO: Log error and retry
            }

            return string.Empty;
        }

        /// <summary>
        /// Helper method used to send http post request.
        /// </summary>
        /// <returns>Response string.</returns>
        private async Task<string> HttpPostHelper(
            string url, 
            HttpContent encodedContent)
        {
            try
            {
                using (HttpResponseMessage httpResponse = 
                    await this.httpClient.PostAsync(url, encodedContent)
                    .ConfigureAwait(false))
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        return await httpResponse.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (WebException e)
            {
                // TODO: Log error and retry
            }

            return string.Empty;
        }
    }
}
