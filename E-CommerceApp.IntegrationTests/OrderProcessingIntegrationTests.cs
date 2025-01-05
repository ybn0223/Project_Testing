using Moq;
using E_CommerceApp.Interfaces;
using E_CommerceApp.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using ECommerceApp;

namespace E_CommerceApp.IntegrationTests
{
    public class OrderProcessingTests
    {
        private readonly HttpClient _httpClient;

        public OrderProcessingTests()
        {
            // Set up HttpClient for making requests to the mock API (Mockoon running on localhost:3000)
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:3000") };
        }

        [Fact]
        public async Task ApplyDiscount_UsingMockAPI_AppliesCorrectDiscount()
        {
            // Arrange: Get the mock order data from Mockoon API
            var response = await _httpClient.GetAsync("/api/orders");
            response.EnsureSuccessStatusCode(); // Ensure the request was successful

            // Read the JSON string from the response
            var jsonString = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON into a concrete Order class
            var order = JsonConvert.DeserializeObject<Order>(jsonString);

            // Ensure that order is not null
            Assert.NotNull(order);

            // Use the concrete Order object in your OrderProcessing class
            var orderProcessing = new OrderProcessingAbstract(order); // Pass the concrete object

            // Act: Apply discount to the order
            orderProcessing.ApplyDiscount(10); // 10% discount

            // Assert: Verify that the total is correct after applying the discount
            Assert.Equal(31.5, order.Total); // Expected total = 35 * (1 - 0.1) = 31.5
            Assert.True(order.DiscountApplied); // Discount flag should be set to true
        }
    }
}