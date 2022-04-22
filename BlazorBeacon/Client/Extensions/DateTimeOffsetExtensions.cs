namespace BlazorBeacon.Client.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static string ToTimeOnly(this DateTimeOffset date) => date.ToString("HH-mm");
    }
}
