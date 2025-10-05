using System.Data.Common;
using PuppeteerSharp;

namespace Scraper.Utils;

// Utility class for web scraping tasks
public static class ScraperUtils
{
  // Launches a headless browser, navigates to the specified URL, and returns the page instance
  public static async Task<IPage> LaunchBrowserPage(string url, bool headless = true)
  {
    var options = new LaunchOptions
    {
      Headless = headless,
      ExecutablePath = Environment.GetEnvironmentVariable("CHROME_PATH") ?? string.Empty,
    };

    var browser = await Puppeteer.LaunchAsync(options);
    var page = await browser.NewPageAsync();
    await page.GoToAsync(url);
    await page.WaitForNetworkIdleAsync();
    return page;
  }

  // Launches a headless browser, navigates to the specified URL, sets up download behavior, and returns the page instance
  public static async Task<IPage> LaunchBrowserPage(string url, string downloadPath, bool headless = true)
  {
    var page = await LaunchBrowserPage(url, headless);
    Directory.CreateDirectory(downloadPath);
    var client = await page.CreateCDPSessionAsync();
    await client.SendAsync("Page.setDownloadBehavior", new
    {
      behavior = "allow",
      downloadPath = downloadPath
    });

    return page;

  }

  // Waits for a file to be downloaded at the specified path, with an optional timeout
  public static async Task<bool> WaitForFileDownload(string filePath, int timeoutMilliseconds = 60000)
  {
    var startTime = DateTime.Now;

    while (!File.Exists(filePath) && (DateTime.Now - startTime).TotalMilliseconds < timeoutMilliseconds)
    {
      await Task.Delay(500); // Check every 500ms
    }

    return File.Exists(filePath);
  }
}