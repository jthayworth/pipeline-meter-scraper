using Scraper.Contracts;
using Scraper.Models;
using Scraper.Utils;

namespace Scraper.Implementation;

public class KinderMorganRunner(ISql sql) : IRunner
{
  public async Task Run(string url, string downloadedName)
  {
    Logger.Log("Navigating to page");
    var downloadPath = Path.Combine(Directory.GetCurrentDirectory(), "downloads");
    var filePath = Path.Combine(downloadPath, downloadedName);

    // creates browser, navigates to page, and sets up the download behavior
    var resourcePage = await ScraperUtils.LaunchBrowserPage(url, downloadPath);

    // gets the button
    Logger.Log("Finding download button");
    var button = await resourcePage.QuerySelectorAsync("input.download-btn");
    if (button == null) throw new Exception("Download button not found on page");

    //clicks the button to start the download
    Logger.Log("Clicking download button");
    await button.ClickAsync();

    // waits for the file to be downloaded, throws if it times out after 60 seconds
    Logger.Log("Waiting for file to download");
    var fileDownloaded = await ScraperUtils.WaitForFileDownload(filePath);
    if (!fileDownloaded) throw new Exception("File download timed out");

    // parse the csv into the KinderMorganRaw model, then map to generic TableItem model
    Logger.Log("Parsing CSV file");
    var items = CsvUtils.ParseCsv<KinderMorganRaw>(filePath).Select(r => new TableItem(r)).ToList();

    // now that we have the generic data model, insert into the db
    Logger.Log("Inserting data into database");
    await sql.InsertData(items);
  }
}