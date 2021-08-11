using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class TurnByTurnService : TurnByTurn.TurnByTurnBase
    {
        public override async Task StartGuidance(GuidanceRequest request, IServerStreamWriter<Step> responseStream, ServerCallContext context)
        {
            var steps = new List<Step>()
            {
                new Step { Direction = "Left", Road = "Clifton" },
                new Step { Direction = "Right", Road = "117th Street" },
                new Step { Direction = "Right", Road = "I90 West" },
                new Step { Direction = "Right", Road = "Smith St." },
                new Step { Direction = "Arrival", Road = "Destination" },
            };

            foreach(var step in steps)
            {
                await Task.Delay(new Random().Next(2000, 5000)); // Simulate processing
                await responseStream.WriteAsync(step);
            }
        }

    }
}
