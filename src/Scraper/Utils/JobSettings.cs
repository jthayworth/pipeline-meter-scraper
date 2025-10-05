namespace Scraper.Utils;

// Class to hold job settings from environment variables
public class JobSettings
{
  public string Type => Environment.GetEnvironmentVariable("JOB_TYPE") ?? string.Empty;

  public string ResourceUrl => Environment.GetEnvironmentVariable("JOB_RESOURCE_URL") ?? string.Empty;

  public string DownloadedName => Environment.GetEnvironmentVariable("JOB_DOWNLOADED_NAME") ?? string.Empty;

  public int Retries => int.TryParse(Environment.GetEnvironmentVariable("JOB_RETRIES"), out int retries) ? retries : 3;

  public int RetryInterval => int.TryParse(Environment.GetEnvironmentVariable("JOB_RETRY_INTERVAL"), out int interval) ? interval : 900;
}