using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("gRPC demo 2000");
            Console.WriteLine("Hit enter when the server looks like it is running!");
            Console.ReadLine();

            Console.WriteLine("What is your name?");
            var name = Console.ReadLine();

            // grpc stuff
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var greeterClient = new Greeter.GreeterClient(channel);

            var requestMessage = new HelloRequest() { Name = name };

            var response = await greeterClient.SayHelloAsync(requestMessage);

            Console.WriteLine($"The server says {response.Message}");

            // ORDER PROCESSOR
            var orderProcessorClient = new ProcessOrder.ProcessOrderClient(channel);

            var orderRequest = new OrderRequest()
            {
                For = "Joe"
            };

            orderRequest.Items.AddRange(new string[] {"Beer", "Eggs", "Bread", "Chips"});

            var orderResponse = await orderProcessorClient.ProcessAsync(orderRequest);
            Console.WriteLine($"Done processing your order.  You can get it at {orderResponse.PickupTime.ToDateTime():d}");


            // 
            Console.WriteLine("Where do you want to go today? ");
            var destination = Console.ReadLine();

            var turnByTurnClient = new TurnByTurn.TurnByTurnClient(channel);

            var turnByTurnRequest = new GuidanceRequest() { Address = destination };

            var streamingResponse = turnByTurnClient.StartGuidance(turnByTurnRequest);

            await foreach(var step in streamingResponse.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine($"Next step: Turn {step.Direction} at {step.Road}");
            }

            Console.WriteLine("You have arrived!");
        }
    }
}
