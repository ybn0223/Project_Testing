using Xunit;
using Moq;
using ECommerceApp;
using E_CommerceApp.Interfaces;
using E_CommerceApp.Models;
using ECommerceApp;

public class OrderProcessingTests
{
    // Test for CalculateTotal() method
    [Fact]
    public void CalculateTotal_ReturnsCorrectTotal()
    {
        // Arrange
        var mockOrder = new Mock<IOrder>();
        var orderItems = new List<IOrderItem>
        {
            new OrderItem { Name = "Item1", Price = 10, Quantity = 2 },
            new OrderItem { Name = "Item2", Price = 5, Quantity = 3 }
        };
        mockOrder.Setup(o => o.Items).Returns(orderItems);

        var orderProcessing = new OrderProcessing(mockOrder.Object);

        // Act
        var total = orderProcessing.CalculateTotal();

        // Assert
        Assert.Equal(35, total); // Expected total = 2 * 10 + 3 * 5 = 35
    }

    // Test for ApplyDiscount() method
    [Fact]
    public void ApplyDiscount_AppliesCorrectDiscount()
    {
        // Arrange
        var mockOrder = new Mock<IOrder>();
        var orderItems = new List<IOrderItem>
    {
        new OrderItem { Name = "Item1", Price = 10, Quantity = 2 },
        new OrderItem { Name = "Item2", Price = 5, Quantity = 3 }
    };
        mockOrder.Setup(o => o.Items).Returns(orderItems);

        // Set up Total and DiscountApplied to allow setting values
        mockOrder.SetupProperty(o => o.Total);
        mockOrder.SetupProperty(o => o.DiscountApplied);

        var orderProcessing = new OrderProcessing(mockOrder.Object);

        // Act
        orderProcessing.ApplyDiscount(10); // Apply 10% discount

        // Assert
        Assert.Equal(31.5, mockOrder.Object.Total); // Total after discount = 35 * (1 - 0.1) = 31.5
        Assert.True(mockOrder.Object.DiscountApplied); // Discount flag should be set to true
    }


    // Test for CheckStock() method
    [Fact]
    public void CheckStock_ReturnsTrue_WhenStockIsSufficient()
    {
        // Arrange
        var mockOrder = new Mock<IOrder>();
        var orderItems = new List<IOrderItem>
        {
            new OrderItem { Name = "Item1", Price = 10, Quantity = 2 },
            new OrderItem { Name = "Item2", Price = 5, Quantity = 3 }
        };
        mockOrder.Setup(o => o.Items).Returns(orderItems);

        var stock = new Dictionary<string, int>
        {
            { "Item1", 5 },
            { "Item2", 5 }
        };

        var orderProcessing = new OrderProcessing(mockOrder.Object);

        // Act
        var result = orderProcessing.CheckStock(stock);

        // Assert
        Assert.True(result); // Stock is sufficient for all items
    }

    [Fact]
    public void CheckStock_ReturnsFalse_WhenStockIsInsufficient()
    {
        // Arrange
        var mockOrder = new Mock<IOrder>();
        var orderItems = new List<IOrderItem>
        {
            new OrderItem { Name = "Item1", Price = 10, Quantity = 2 },
            new OrderItem { Name = "Item2", Price = 5, Quantity = 3 }
        };
        mockOrder.Setup(o => o.Items).Returns(orderItems);

        var stock = new Dictionary<string, int>
        {
            { "Item1", 1 }, // Not enough stock for Item1
            { "Item2", 5 }
        };

        var orderProcessing = new OrderProcessing(mockOrder.Object);

        // Act
        var result = orderProcessing.CheckStock(stock);

        // Assert
        Assert.False(result); // Not enough stock for Item1
    }
}