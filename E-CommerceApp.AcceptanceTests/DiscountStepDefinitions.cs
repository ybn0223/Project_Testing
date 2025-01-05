using Xunit.Gherkin.Quick;
using E_CommerceApp.Models;
using E_CommerceApp.Interfaces;
using System.Linq;
using Xunit;
using ECommerceApp;

namespace E_CommerceApp.AcceptanceTests.StepDefinitions
{
    [FeatureFile("../Features/Discount.feature")]
    public class DiscountStepDefinitions : Feature
    {
        private IOrder _order;  // Declare the field here
        private double _discountPercentage;

        // Given step: Setup order with items
        [Given(@"the order has items with prices and quantities")]
        public void GivenTheOrderHasItemsWithPricesAndQuantities()
        {
            Order _order = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { Name = "Item1", Price = 10, Quantity = 2 },
                    new OrderItem { Name = "Item2", Price = 5, Quantity = 3 }
                }
            };
        }

        // When step: Apply discount
        [When(@"the discount of (.*)% is applied")]
        public void WhenTheDiscountIsApplied(double discountPercentage)
        {
            _discountPercentage = discountPercentage;
            var orderProcessing = new OrderProcessing(_order);
            orderProcessing.ApplyDiscount(_discountPercentage);
        }

        // Then step: Verify total price after discount
        [Then(@"the total price of the order should be (.*)")]
        public void ThenTheTotalPriceOfTheOrderShouldBe(double expectedTotal)
        {
            var actualTotal = _order.Items.Sum(item => item.Price * item.Quantity) * (1 - (_discountPercentage / 100));
            Assert.Equal(expectedTotal, actualTotal);
        }

        // And step: Verify discount flag
        [Then(@"the discount flag should be (.*)")]
        public void ThenTheDiscountFlagShouldBe(bool expectedDiscountApplied)
        {
            Assert.Equal(expectedDiscountApplied, _order.DiscountApplied);
        }
    }
}