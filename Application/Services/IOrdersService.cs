using Application.DTOs;
using Domain.Entities;

namespace Application.Services;

public interface IOrdersService
{
    Task<OrderDto> GetOrderByIdAsync(int id);
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
    Task<OrderDto> UpdateOrderAsync(OrderDto orderDto);
    Task DeleteOrderAsync(int id);
    Task<IEnumerable<OrderDto>> GetOrdersByDatesAsync(DateTime start, DateTime end);
    Task<IEnumerable<ItemSoldStatisticsDto>> GetItemsSoldByDayAsync(DateTime day);
}

