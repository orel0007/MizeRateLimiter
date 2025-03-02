# MizeRateLimiter

## Project Overview
**Owner:** Orel Aviad  
**Description:**  
MizeRateLimiter is a rate-limiting solution designed to control how often an action can be performed within a fixed time window. This ensures API stability, prevents overload, and optimizes performance.

---

## Key Decisions & Justifications

### 1. **Using an Absolute Window for Rate Limiting**
- An absolute window enforces strict request limits per defined time intervals (e.g., 100 requests per hour, resetting every full hour).
- This ensures predictable behavior and aligns with many API rate-limiting standards.

### 2. **Using `SemaphoreSlim` Instead of `lock` or `Mutex`**
- `lock` does not support `async/await`, making it unsuitable for asynchronous operations.
- `Mutex` is slower and introduces unnecessary overhead.
- `SemaphoreSlim` is optimized for async scenarios and provides better concurrency control.

### 3. **Running Rate Limits in Parallel**
- Improves performance by reducing execution time.
- Uses `Task.WhenAll()` to process multiple rate limit checks simultaneously.

```csharp
var limitTasks = rateLimits.Select(rl => rl.EnsureLimitAsync());
await Task.WhenAll(limitTasks);

