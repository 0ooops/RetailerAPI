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
        public void TestCalculateTrolleyTotalWithInvalidJObject()
        {
            // Prepare
            JObject testObject = new JObject();

            // Set up behavior
            Mock<IResourceService> resourceService = new Mock<IResourceService>();

            // Call method and assert
            TrolleyTotalController trolleyController =
                new TrolleyTotalController(resourceService.Object);
            Assert.Throws<ArgumentException>(() => trolleyController.Post(testObject));
        }

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