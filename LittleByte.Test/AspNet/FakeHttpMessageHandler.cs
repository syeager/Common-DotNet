using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LittleByte.Test.AspNet
{
    // https://dev.to/n_develop/mocking-the-httpclient-in-net-core-with-nsubstitute-k4j
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private string response = string.Empty;
        private HttpStatusCode statusCode = HttpStatusCode.OK;

        public int NumberOfCalls { get; private set; }
        public Exception? Exception { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            NumberOfCalls++;

            if(Exception != null)
            {
                throw Exception;
            }

            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(response)
            };
            var taskResult = Task.FromResult(httpResponseMessage);
            return taskResult;
        }

        public void SetResponse(HttpStatusCode statusCode, string response)
        {
            this.statusCode = statusCode;
            this.response = response;
        }

        public static HttpClient Create(out FakeHttpMessageHandler httpMessageHandler)
        {
            httpMessageHandler = new FakeHttpMessageHandler();
            return new HttpClient(httpMessageHandler);
        }
    }
}