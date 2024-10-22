using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Application.Services;
using Application.DTOs;

namespace SampleShopV2
{
    public class OrdersFunction
    {
        private readonly IOrdersService _ordersService;

        public OrdersFunction(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [FunctionName("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "orders")] HttpRequest req)
        {
            var orders = await _ordersService.GetAllOrdersAsync();
            return new OkObjectResult(orders);
        }

        [FunctionName("GetOrderById")]
        public async Task<IActionResult> GetOrderById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "orders/{id}")] HttpRequest req,
            int id)
        {
            var order = await _ordersService.GetOrderByIdAsync(id);
            return order != null ? new OkObjectResult(order) : new NotFoundResult();
        }

        [FunctionName("GetOrdersByDates")]
        public async Task<IActionResult> GetOrdersByDates(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "orders/dates")] HttpRequest req)
        {
            string startDateString = req.Query["start"];
            string endDateString = req.Query["end"];

            if (string.IsNullOrEmpty(startDateString) || string.IsNullOrEmpty(endDateString))
            {
                return new BadRequestObjectResult("Start and end dates must be provided.");
            }

            if (!DateTime.TryParse(startDateString, out DateTime startDate) || !DateTime.TryParse(endDateString, out DateTime endDate))
            {
                return new BadRequestObjectResult("Invalid date format. Please use a valid date.");
            }

            var orders = await _ordersService.GetOrdersByDatesAsync(startDate, endDate);
            return new OkObjectResult(orders);
        }

        [FunctionName("GetItemsSoldByDay")]
        public async Task<IActionResult> GetItemsSoldByDay(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "orders/items")] HttpRequest req)
        {
            string dayString = req.Query["day"];

            if (string.IsNullOrWhiteSpace(dayString))
            {
                return new BadRequestObjectResult("Day must be provided.");
            }

            if (!DateTime.TryParse(dayString, out DateTime day))
            {
                return new BadRequestObjectResult("Invalid date format. Please use a valid date.");
            }

            var orders = await _ordersService.GetItemsSoldByDayAsync(day);
            return new OkObjectResult(orders);
        }

        [FunctionName("PostOrder")]
        public async Task<IActionResult> PostOrder(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "orders")] HttpRequest req)
        {
            var order = await System.Text.Json.JsonSerializer.DeserializeAsync<OrderDto>(req.Body);

            if (order == null)
            {
                return new BadRequestObjectResult("Invalid order data.");
            }

            var createdOrder = await _ordersService.CreateOrderAsync(order);
            return new CreatedResult($"/orders/{createdOrder.Id}", createdOrder);
        }

        [FunctionName("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "orders/{id}")] HttpRequest req,
            int id)
        {
            await _ordersService.DeleteOrderAsync(id);
            return new OkObjectResult("Order deleted.");
        }
    }
}
