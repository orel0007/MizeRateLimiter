# MizeRateLimiter

## Project Overview
**Owner:** Orel Aviad  
**Description:**  
MizeRateLimiter is a rate-limiting solution designed to control how often an action can be performed within a fixed time window. This ensures API stability, prevents overload, and optimizes performance.

---

## Key Decisions & Justifications

### 1. **Sliding Window Implementation**
A sliding window approach ensures a more even distribution of requests over time, preventing bursts that could overwhelm the system.
- exception: if Api have permanent limits per time(day,month) so i had choose absulte window.

### 2. **Using `SemaphoreSlim` Instead of `lock` or `Mutex`**
- `lock` does not support `async/await`, mutex is too low


