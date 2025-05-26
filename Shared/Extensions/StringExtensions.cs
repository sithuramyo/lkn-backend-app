namespace Shared.Extensions;

public static class StringExtensions
{
    public static string GetCode(this string str, int count, string digit)
    {
        var formattedNumber = (count + 1).ToString(digit);
        return $"{str}{formattedNumber}";
    }

    public static DateTime ToMyanmarTime(this DateTime dateTime)
    {
        var myanmarTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time");
        var myanmarDateTime = TimeZoneInfo.ConvertTime(dateTime, myanmarTimeZone);
        return myanmarDateTime;
    }
}