using Domain.Entities;
using SampleShopV2.Tests.Builder;

namespace SampleShopV2.Tests
{
    public class OrderTests
    {
        [Fact]
        public void Should_Create_Order_With_Current_Date()
        {
            // Arrange
            var order = OrderBuilder.BuildOrder(1);

            // Act
            var createDate = order.CreateDate;

            // Assert
            Assert.Equal(DateTime.UtcNow.Date, createDate.Date);
        }

        [Fact]
        public void Should_Add_OrderItem_To_Order()
        {
            // Arrange
            var order = OrderBuilder.BuildOrder(0);

            // Act
            order.AddOrderItem("Laptop", "Dell", 1200.00m, 2);

            // Assert
            Assert.Single(order.OrderItems); 
            var item = order.OrderItems.First();
            Assert.Equal("Laptop", item.Name);
            Assert.Equal(1200.00m, item.Price);
            Assert.Equal(2, item.Quantity);
        }

        [Fact]
        public void Should_Update_OrderItem_Quantity()
        {
            // Arrange
            var order = OrderBuilder.BuildOrder(1);
            order.AddOrderItem("Mouse", "Logitech", 50.00m, 1);
            var itemId = order.OrderItems.First().Id;

            // Act
            order.UpdateOrderItem(itemId, 3);

            // Assert
            var item = order.OrderItems.First();
            Assert.Equal(3, item.Quantity);
        }

        [Fact]
        public void Should_Remove_OrderItem_From_Order()
        {
            // Arrange
            var order = OrderBuilder.BuildOrder(0);
            order.AddOrderItem("Keyboard", "Logitech", 100.00m, 1);
            var itemId = order.OrderItems.First().Id;

            // Act
            order.RemoveOrderItem(itemId);

            // Assert
            Assert.Empty(order.OrderItems); 
        }

        [Fact]
        public void Should_Calculate_TotalPrice_Of_Order()
        {
            // Arrange
            var order = OrderBuilder.BuildOrder(0);
            order.AddOrderItem("Laptop", "Dell", 1200.00m, 2);  // 2400
            order.AddOrderItem("Mouse", "Logitech", 50.00m, 1);  // 50

            // Act
            var totalPrice = order.GetTotalPrice();

            // Assert
            Assert.Equal(2450.00m, totalPrice); 
        }

        [Fact]
        public void Should_Throw_Exception_When_Adding_Item_With_Invalid_Quantity()
        {
            // Arrange
            var order = OrderBuilder.BuildOrder(1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => order.AddOrderItem("Laptop", "Dell", 1200.00m, 0));
        }

        [Fact]
        public void Should_Throw_Exception_When_Updating_Nonexistent_OrderItem()
        {
            // Arrange
            var order = OrderBuilder.BuildOrder(1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => order.UpdateOrderItem(999, 3)); // Non existent item
        }

        [Fact]
        public void Should_Throw_Exception_When_Removing_Nonexistent_OrderItem()
        {
            // Arrange
            var order = OrderBuilder.BuildOrder(1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => order.RemoveOrderItem(999)); // Non existent item
        }
    }
}
