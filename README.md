# MizeRateLimiter-
owner: Orel Aviad
project way to control how many times an action can be performed in a given time window. This rate limiter helps manage API requests, prevent overload, and ensure smooth execution.
##-
Using SemaphoreSlim Instead of lock or Mutex
lock does not support async/await, and muter is slow.
###
Running All Rate Limits in Parallel for better performence
####
 Handling Old Requests in ConcurrentQueue<DateTime> its safe threads and Fifo so always can pop on the oldest element without search
#####


