namespace Scraper.Utils;

public static class DateUtils
{
  // Parses a date string in the specified format and returns a DateTime object, or null if parsing fails
  public static DateTime? ParseDateString(string dateString, string format)
  {
    if (DateTime.TryParseExact(dateString, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var date))
    {
      return date;
    }

    return null;
  }
}