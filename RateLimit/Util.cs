using System;
namespace RateLimit
{
    public static class Util
    {
        public static TimeSpan ToTimeSpan(this string timeSpan)
        {
            var length = timeSpan.Length - 1;
            var time = timeSpan.Substring(0, length);
            var unit = timeSpan.Substring(length, 1);

            return unit switch
            {
                "d" => TimeSpan.FromDays(double.Parse(time)),
                "h" => TimeSpan.FromHours(double.Parse(time)),
                "m" => TimeSpan.FromMinutes(double.Parse(time)),
                "s" => TimeSpan.FromSeconds(double.Parse(time)),
                _ => throw new FormatException($"{timeSpan} unable to convert, unkonwn type {unit}"),
            };
        }

        public static string RetryAfterFrom(this DateTime timestamp, TimeSpan timeSpan)
        {
            var diff = timestamp + timeSpan - DateTime.UtcNow;
            var seconds = Math.Max(diff.TotalSeconds, 1);

            return $"{seconds:F0}";
        }
    }
}
