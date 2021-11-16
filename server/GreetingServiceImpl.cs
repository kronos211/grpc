using System;
using System.Threading.Tasks;
using Greet;
using Grpc.Core;
using static Greet.GreetingService;

namespace server
{
    public class GreetingServiceImpl : GreetingServiceBase
    {
        public override async Task GreetEveryone(IAsyncStreamReader<GreetEveryoneRequest> requestStream, IServerStreamWriter<GreetEveryoneResponse> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                var result = String.Format("Hellow {0}{1}",
                requestStream.Current.Greeting.FirstName,
                requestStream.Current.Greeting.LastName);       

                System.Console.WriteLine("Received" + result);  
                await responseStream.WriteAsync(new GreetEveryoneResponse { Result = result});        
            }
        }
    }
}