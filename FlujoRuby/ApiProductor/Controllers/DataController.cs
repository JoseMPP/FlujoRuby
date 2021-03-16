
namespace ApiProductor.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Azure.Messaging.ServiceBus;
    using ApiProductor.Models;
    using Newtonsoft.Json;

    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpPost]
        public async Task<bool> EnviarAsync([FromBody] Data data)
        {
            string connectionString = "Endpoint=sb://josepizarro.servicebus.windows.net/;SharedAccessKeyName=Enviar;SharedAccessKey=8qeSdRhy0tQhFUhCTsjVDGXssU3Ai9CQX+jlag8up8Q=;EntityPath=cola1";
            string queueName = "cola1";
            string mensaje = JsonConvert.SerializeObject(data);

            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender(queueName);

                // create a message that we can send
                ServiceBusMessage message = new ServiceBusMessage(mensaje);

                // send the message
                await sender.SendMessageAsync(message);
                Console.WriteLine($"Sent a single message to the queue: {queueName}");
            }
            return true;
        }
    }
}