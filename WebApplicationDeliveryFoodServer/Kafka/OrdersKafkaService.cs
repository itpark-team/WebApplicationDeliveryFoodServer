using System.Text.Json;
using Confluent.Kafka;
using WebApplicationDeliveryFoodServer.Models;

namespace WebApplicationDeliveryFoodServer.Kafka;

public class OrdersKafkaService
{
    private IProducer<Null, string> _producer;

    public OrdersKafkaService(ProducerConfig producerConfig)
    {
        _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
    }

    public void SendNotifyToTelegram(Order order)
    {
        string orderAsJson = JsonSerializer.Serialize(order);

        _producer.Produce("tgqueue", new Message<Null, string> { Value = orderAsJson });
        _producer.Flush(TimeSpan.FromSeconds(3));
    }
}