using Microsoft.AspNetCore.Mvc;
using WebApplicationDeliveryFoodServer.Kafka;
using WebApplicationDeliveryFoodServer.Models;

namespace WebApplicationDeliveryFoodServer.Controllers;

[ApiController]
[Route("orders/")]
public class OrdersController : ControllerBase
{
    private OrdersKafkaService _ordersKafkaService;

    public OrdersController(OrdersKafkaService ordersKafkaService)
    {
        _ordersKafkaService = ordersKafkaService;
    }

    [HttpPost("create")]
    public void Create(Order order)
    {
        Random random = new Random();
        order.Id = random.Next(1, 1000000);
        
        _ordersKafkaService.SendNotifyToTelegram(order);
    }
}