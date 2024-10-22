using Domain.Repositories;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;


        public OrdersService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id, i => i.OrderItems);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync(i => i.OrderItems);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> UpdateOrderAsync(OrderDto orderDto)
        {
            var orderDb = await _orderRepository.GetByIdAsync(orderDto.Id);

            if (orderDb == null)
                throw new KeyNotFoundException($"Order with Id {orderDto.Id} not found.");

            _mapper.Map(orderDto, orderDb);

            await _orderRepository.UpdateAsync(orderDb);
            await _orderRepository.SaveChangesAsync();

            return _mapper.Map<OrderDto>(orderDb);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order != null)
            {
                await _orderRepository.DeleteAsync(order);
                await _orderRepository.SaveChangesAsync();
            }
        }
        
        public async Task<IEnumerable<OrderDto>> GetOrdersByDatesAsync(DateTime start, DateTime end)
        {
            if (start == DateTime.MinValue || end == DateTime.MinValue)
                throw new ArgumentException("Start and end dates must be valid dates.");

            if (start.Date >= DateTime.Now.Date || end.Date >= DateTime.Now.Date)
                throw new ArgumentException("Start and end dates must be in the past.");

            if (start > end)
                throw new ArgumentException("Start date cannot be after the end date.");

            var orders = await _orderRepository.GetByDatesAsync(start, end);

            return orders.Select(order => new OrderDto
            {
                Id = order.Id,
                CreateDate = order.CreateDate,
                TotalPrice = order.GetTotalPrice()
            }).ToList();
        }

        public async Task<IEnumerable<ItemSoldStatisticsDto>> GetItemsSoldByDayAsync(DateTime day)
        {
            if (day == DateTime.MinValue)
                throw new ArgumentException("The day must be a valid date.");

            if (day.Date >= DateTime.Now.Date)
                throw new ArgumentException("The day must be in the past.");

            var ordersOnDay = await _orderRepository.GetOrdersByDayAsync(day);

            var itemSoldStatistics = ordersOnDay
                .SelectMany(order => order.OrderItems)
                .GroupBy(orderItem => orderItem.OrderId)
                .Select(group =>
                {
                    var orderId = group.Key;

                    return new ItemSoldStatisticsDto
                    {
                        OrderId = orderId,
                        TotalQuantity = group.Sum(orderItem => orderItem.Quantity),
                        TotalPrice = group.Sum(orderItem => orderItem.Quantity * orderItem.Price)
                    };
                })
                .ToList();

            return itemSoldStatistics;
        }
    }
}