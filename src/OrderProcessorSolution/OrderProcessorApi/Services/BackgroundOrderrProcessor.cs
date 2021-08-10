using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderProcessorApi.Services
{
    public class BackgroundOrderrProcessor : BackgroundService
    {
        private readonly ILogger<BackgroundOrderrProcessor> _logger;
        private readonly OrderProcessorChannel _channel;

        public BackgroundOrderrProcessor(ILogger<BackgroundOrderrProcessor> logger, OrderProcessorChannel channel)
        {
            _logger = logger;
            _channel = channel;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach(var orderItems in _channel.ReadAllAsync(stoppingToken))
            {
                foreach(var item in orderItems)
                {
                    _logger.LogInformation($"Processing {item}");
                    await Task.Delay(1000); // fake word
                }                
            }
        }
    }
}
