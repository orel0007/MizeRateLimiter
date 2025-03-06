using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MizeRateLimiter.RateLimits
{
    public class RateLimit
    {
        public int Limit { get; }
        public TimeSpan Interval { get; }
        private readonly ConcurrentQueue<DateTime> requests;
        private readonly SemaphoreSlim semaphore = new(1, 1);

        public RateLimit(int limit, TimeSpan interval)
        {
            Limit = limit;
            Interval = interval;
            requests = new ConcurrentQueue<DateTime>();
        }

        public async Task EnsureLimitAsync()
        {
            await semaphore.WaitAsync();
            try
            {
                DateTime now = DateTime.UtcNow;
                DateTime windowStart = now - Interval;

                while (requests.TryPeek(out DateTime oldest) && oldest < windowStart)
                {
                    requests.TryDequeue(out _);
                }

                if (requests.Count < Limit)
                {
                    requests.Enqueue(now);
                    return;
                }
                await Task.Delay(Interval / Limit);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
