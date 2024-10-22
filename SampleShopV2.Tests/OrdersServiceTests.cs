using SampleShopV2.Tests.Base;
using Application.Services;
using Infrastructure.Repositories;
using SampleShopV2.Tests.Builder;

namespace SampleShopV2.Tests;

public class OrdersServiceTests : TestBase
{
    [Fact]
    public async Task When_GetByDatesRunsWithNonExistingOrderDates_EmptyCollectionIsReturned()
    {
        // Arrange
        await using var context = CreateDbContext();
        var orderRepository = new OrderRepository(context);
        var mapper = CreateMapper();

        var service = new OrdersService(orderRepository, mapper);

        // Act
        var result = await service.GetOrdersByDatesAsync(new DateTime(2017, 9, 1), new DateTime(2018, 1, 1));

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task When_GetByDatesRunsWithExistingOrderDates_ExistingOrdersAreReturned()
    {
        // Arrange
        await using var context = CreateDbContext();
        var orderRepository = new OrderRepository(context);
        var mapper = CreateMapper();

        var order1 = OrderBuilder.BuildOrder(new DateTime(2018, 1, 2), 2);
        await orderRepository.AddAsync(order1);
        
        var order2 = OrderBuilder.BuildOrder(new DateTime(2018, 1, 3), 3);
        await orderRepository.AddAsync(order2);

        var order3 = OrderBuilder.BuildOrder(new DateTime(2018, 1, 4), 4);
        await orderRepository.AddAsync(order3);

        await orderRepository.SaveChangesAsync();

        var service = new OrdersService(orderRepository, mapper);

        // Act
        var result = await service.GetOrdersByDatesAsync(new DateTime(2018, 1, 1), new DateTime(2018, 3, 1));

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task When_GetItemsSoldByDayRunsWithNonExistingOrdersDate_EmptyCollectionReturned()
    {
        // Arrange
        await using var context = CreateDbContext();
        var orderRepository = new OrderRepository(context);
        var mapper = CreateMapper();

        var order1 = OrderBuilder.BuildOrder(new DateTime(2020, 6, 3), 3);
        await orderRepository.AddAsync(order1);

        await orderRepository.SaveChangesAsync();

        var service = new OrdersService(orderRepository, mapper);

        // Act
        var result = await service.GetItemsSoldByDayAsync(new DateTime(2017, 6, 3));

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task When_GetItemsSoldByDayRunsWithExistingOrdersDate_AllItemsSoldAreReturned()
    {
        // Arrange
        await using var context = CreateDbContext();
        var orderRepository = new OrderRepository(context);
        var mapper = CreateMapper();

        var order1 = OrderBuilder.BuildOrder(new DateTime(2018, 3, 3), 1);
        await orderRepository.AddAsync(order1);

        var order2 = OrderBuilder.BuildOrder(new DateTime(2018, 3, 3), 2);
        await orderRepository.AddAsync(order2);

        var order3 = OrderBuilder.BuildOrder(new DateTime(2018, 3, 3), 3);
        await orderRepository.AddAsync(order3);

        var order4 = OrderBuilder.BuildOrder(new DateTime(2018, 3, 3), 4);
        await orderRepository.AddAsync(order4);

        await orderRepository.SaveChangesAsync();

        var service = new OrdersService(orderRepository, mapper);

        // Act
        var result = await service.GetItemsSoldByDayAsync(new DateTime(2018, 3, 3));

        // Assert
        Assert.Equal(4, result.Count());
    }
}