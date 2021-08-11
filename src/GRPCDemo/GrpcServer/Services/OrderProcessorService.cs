using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class OrderProcessorService : ProcessOrder.ProcessOrderBase
    {
        private readonly ILogger<OrderProcessorService> _logger;

        public OrderProcessorService(ILogger<OrderProcessorService> logger)
        {
            _logger = logger;
        }

        public override async Task<OrderResponse> Process(OrderRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Got an order for {request.For}");

            foreach (var item in request.Items)
            {
                await Task.Delay(1000); // simulate processing
                _logger.LogInformation($"\tProcessing item {item}...");
            }

            return new OrderResponse
            {
                Id = new Random().Next(500, 10000).ToString(),
                PickupTime = Timestamp.FromDateTime(DateTime.Now.AddDays(3).ToUniversalTime())
            };
        }
    }
}
