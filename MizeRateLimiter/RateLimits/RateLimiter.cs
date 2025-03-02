using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MizeRateLimiter.RateLimits
{
    public class RateLimiter<TArg>
    {
        private readonly Func<TArg, Task> action;
        private readonly List<RateLimit> rateLimits;
        private readonly SemaphoreSlim semaphore = new(1, 1);

        public RateLimiter(Func<TArg, Task> action, params RateLimit[] rateLimits)
        {
            this.action = action;
            this.rateLimits = rateLimits.ToList();
        }

        public async Task Perform(TArg argument)
        {
            await semaphore.WaitAsync();
            try
            {
                var limitTasks = rateLimits.Select(rl => rl.EnsureLimitAsync());
                await Task.WhenAll(limitTasks);
                await action(argument);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
