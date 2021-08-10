using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OrderProcessorApi.Services
{
    public class OrderProcessorChannel
    {
        private const int MaxMessagesInChannel = 100;
        private readonly Channel<List<string>> _channel;

        public OrderProcessorChannel()
        {
            var options = new BoundedChannelOptions(MaxMessagesInChannel)
            {
                SingleReader = true,
                SingleWriter = false
            };

            _channel = Channel.CreateBounded<List<string>>(options);
        }

        public async Task<bool> AddOrdersToProcess(List<string> orders, CancellationToken ct = default)
        {
            while(await _channel.Writer.WaitToWriteAsync(ct) && !ct.IsCancellationRequested)
            {
                if(_channel.Writer.TryWrite(orders))
                {
                    return true;
                }
            }

            return false;
        }

        public IAsyncEnumerable<List<string>> ReadAllAsync(CancellationToken ct = default)
        {
            return _channel.Reader.ReadAllAsync();
        }
    }
}
