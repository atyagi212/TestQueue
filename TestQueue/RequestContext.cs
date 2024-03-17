using System;
namespace TestQueue
{
	public class RequestContext
    {
        private readonly RequestDelegate _next;
        private readonly UserRequestQueue _userQueue;

        public RequestContext(RequestDelegate next, UserRequestQueue userQueue)
        {
            _next = next;
            _userQueue = userQueue;
        }

        public async Task Invoke(HttpContext context)
        {
            _userQueue.IncrementUserCount();
            try
            {
                await _userQueue.EnqueueRequest(this);
            }
            finally
            {
                _userQueue.DecrementUserCount();
            }
        }

        public async Task ServeRequest()
        {
            Utility.serveRequest = true;
            //await _next.Invoke(HttpContext);
        }

    }
}

