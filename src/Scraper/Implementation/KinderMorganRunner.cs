using Scraper.Contracts;
using Scraper.Models;
using Scraper.Utils;

namespace Scraper.Implementation;

public class KinderMorganRunner(ISql sql) : IRunner
{
  public async Task Run(string url, string downloadedName)
  {
    var downloadPath = Path.Combine(Directory.GetCurrentDirectory(), "downloads");
    var filePath = Path.Combine(downloadPath, downloadedName);

    // creates browser, navigates to page, and sets up the download behavior
    var resourcePage = await ScraperUtils.LaunchBrowserPage(url, downloadPath);

    // gets the button
    var button = await resourcePage.QuerySelectorAsync("input.download-btn");
    if (button == null) throw new Exception("Download button not found on page");

    //clicks the button to start the download
    await button.ClickAsync();

    // waits for the file to be downloaded, throws if it times out after 60 seconds
    var fileDownloaded = await ScraperUtils.WaitForFileDownload(filePath);
    if (!fileDownloaded) throw new Exception("File download timed out");

    // parse the csv into the KinderMorganRaw model, then map to generic TableItem model
    var items = CsvUtils.ParseCsv<KinderMorganRaw>(filePath).Select(r => new TableItem(r)).ToList();

    // now that we have the generic data model, insert into the db
    await sql.InsertData(items);
  }
}