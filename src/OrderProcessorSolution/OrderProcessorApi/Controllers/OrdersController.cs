using Microsoft.AspNetCore.Mvc;
using OrderProcessorApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProcessorApi.Controllers
{
    public class OrdersController : ControllerBase
    {
        private readonly OrderProcessorChannel _channel;

        public OrdersController(OrderProcessorChannel channel)
        {
            _channel = channel;
        }

        [HttpPost("orders")]
        public async Task<ActionResult> AddAnOrder([FromBody] OrderRequest request)
        {
            var numberOfItems = request.Items.Count;
            var worked = await _channel.AddOrdersToProcess(request.Items);
            if(worked)
            {
                return Ok(request);
            }

            return BadRequest("Something horrible happened");
        }
    }

    public record OrderRequest
    {
        public string Customer { get; set; }
        public List<string> Items { get; set; }
    }


}
