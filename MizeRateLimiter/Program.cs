using MizeRateLimiter.RateLimits;
class Program
{
    static async Task Main()
    {
        Func<int, Task> exampleAction = async (int value) =>
        {
            Console.WriteLine($"Executing action with value {value} at {DateTime.UtcNow:HH:mm:ss.fff}");
            await Task.Delay(100);
        };

        var rateLimiter = new RateLimiter<int>(
            exampleAction,
            new RateLimit(10, TimeSpan.FromSeconds(1)), 
            new RateLimit(100, TimeSpan.FromMinutes(1)),
            new RateLimit(1000, TimeSpan.FromDays(1))    
        );

        var tasks = Enumerable.Range(1, 50).Select(i => rateLimiter.Perform(i));
        await Task.WhenAll(tasks);
    }
}