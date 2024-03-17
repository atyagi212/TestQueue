using System;
namespace TestQueue
{
	public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserRequestQueue _userQueue;
        private readonly Queue<RequestContext> _queue = new Queue<RequestContext>();
        private int _userCount = 0;
        private static int _maxUserCount = 2;

        public RequestMiddleware(RequestDelegate next, UserRequestQueue userQueue)
        {
            _next = next;
            _userQueue = userQueue;
        }

        public async Task Invoke(HttpContext context)
        {
            Utility.serveRequest = false;
            RequestContext requestContext = new RequestContext(_next, _userQueue);
            await requestContext.Invoke(context);
            //await _userQueue.ServeNextRequest();
            if (Utility.serveRequest)
                await _next.Invoke(context);
        }


        public async Task EnqueueRequest(RequestContext context)
        {
            try
            {
                if (_userCount < 2)
                {
                    // Serve the request immediately
                    await context.ServeRequest();
                }
                else
                {
                    // Put request in queue
                    _queue.Enqueue(context);
                }
            }
            finally
            {
            }
        }

        public void IncrementUserCount()
        {
            Interlocked.Increment(ref _userCount);
        }

        public void DecrementUserCount()
        {
            //Interlocked.Decrement(ref _userCount);
        }
    }
}

