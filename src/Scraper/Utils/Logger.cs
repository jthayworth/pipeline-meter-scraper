namespace Scraper.Utils;

// Simple logger utility for logging messages and exceptions with timestamps
internal static class Logger
{
  internal static void Log(Exception ex)
  {
    string message = ex.ToString();
    Log(message);
  }

  internal static void Log(string message)
  {
    string timestamp = WriteTimestamp();
    Console.WriteLine($"{timestamp}: {message}");
  }

  internal static string WriteTimestamp()
  {
    return DateTime.Now.ToString("u");
  }
}