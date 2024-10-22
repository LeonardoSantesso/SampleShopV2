using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(SampleShopV2Context context) : base(context) { }

        public async Task<IEnumerable<Order>> GetByDatesAsync(DateTime start, DateTime end)
        {
            return await _context.Set<Order>()
                .Include(o => o.OrderItems)
                .Where(order => order.CreateDate >= start && order.CreateDate <= end)
                .OrderBy(order => order.CreateDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByDayAsync(DateTime day)
        {
            return await _context.Set<Order>()
                .Include(o => o.OrderItems)
                .Where(order => order.CreateDate.Date == day.Date)
                .ToListAsync();
        }
    }
}