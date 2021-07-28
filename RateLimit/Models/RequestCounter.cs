using System;

namespace RateLimit
{
    /// <summary>
    /// Count request with time stamp
    /// </summary>
    public class RequestCounter
    {
        public DateTime Timestamp { get; set; }
        public double Count { get; set; }
    }
}
