using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SampleShopV2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _ordersService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _ordersService.GetOrderByIdAsync(id);
            return order != null ? Ok(order) : NotFound();
        }

        [HttpGet("dates")]
        public async Task<IActionResult> GetOrdersByDates([FromQuery] string start, [FromQuery] string end)
        {
            if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
            {
                return BadRequest("Start and end dates must be provided.");
            }

            if (!DateTime.TryParse(start, out DateTime startDate) || !DateTime.TryParse(end, out DateTime endDate))
            {
                return BadRequest("Invalid date format. Please use a valid date.");
            }

            var orders = await _ordersService.GetOrdersByDatesAsync(startDate, endDate);
            return Ok(orders);
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetItemsSoldByDay([FromQuery] string day)
        {
            if (string.IsNullOrWhiteSpace(day))
            {
                return BadRequest("Day must be provided.");
            }

            if (!DateTime.TryParse(day, out DateTime parsedDay))
            {
                return BadRequest("Invalid date format. Please use a valid date.");
            }

            var orders = await _ordersService.GetItemsSoldByDayAsync(parsedDay);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] OrderDto order)
        {
            if (order == null)
            {
                return BadRequest("Invalid order data.");
            }

            var createdOrder = await _ordersService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _ordersService.DeleteOrderAsync(id);
            return Ok("Order deleted.");
        }
    }
}
