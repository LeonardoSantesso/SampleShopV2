using Domain.Entities;

namespace Domain.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetByDatesAsync(DateTime start, DateTime end);
    Task<IEnumerable<Order>> GetOrdersByDayAsync(DateTime day);
}
