using System;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RetailerAPI.Services;

namespace RetailerAPI.Controllers.Tests
{
    public class TrolleyTotalControllerTest
    {
        [Test]
        public void TestCalculateTrolleyTotalWithValidJObject()
        {
            // Prepare
            double expectedResult = 2.8;
            string trolleyStr = @"
            {
                'products': [
                {
                    'name': 'string',
                    'price': 3
                }
                ],
                'specials': [
                {
                    'quantities': [
                    {
                        'name': 'string',
                        'quantity': 1
                    }
                    ],
                    'total': 1.4
                }
                ],
                'quantities': [
                {
                    'name': 'string',
                    'quantity': 2
                }
                ]
            }";
            JObject trolley = JObject.Parse(trolleyStr);

            // Set up behavior
            Mock<IResourceService> resourceService = new Mock<IResourceService>();
            resourceService.Setup(r => r.GetMinimumTotal(trolley)).Returns(expectedResult);

            // Call method and assert
            TrolleyTotalController trolleyController =
                new TrolleyTotalController(resourceService.Object);
            Assert.AreEqual(expectedResult, trolleyController.Post(trolley).Value);
        }
    }
}