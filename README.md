# 📌 MizeRateLimiter
**Owner:** Orel Aviad  
**Project Goal:** A rate limiter to control how many times an action can be performed in a given time window.  
This helps manage API requests, prevent overload, and ensure smooth execution.

---

## **💡 Key Decisions & Why**
### **1️⃣ Using `SemaphoreSlim` Instead of `lock` or `Mutex`**
- `lock` **does not support `async/await`**.
- `Mutex` **is slow** and meant for cross-process locks, which I don't need.
- ✅ **Final Choice:** `SemaphoreSlim(1,1)`, as it allows **async operations** and ensures only one execution at a time.

---

### **2️⃣ Running All Rate Limits in Parallel for Better Performance**
```csharp
var limitTasks = rateLimits.Select(rl => rl.EnsureLimitAsync());
await Task.WhenAll(limitTasks);
