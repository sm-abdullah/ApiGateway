using System;

namespace RateLimit.Models
{
    public class RuleLimit
    {
        public string Key { get; set; }
        public int Count { get; set; }
        public TimeSpan TimeSpan { get; set; }
    }
}
