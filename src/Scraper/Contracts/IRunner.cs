namespace Scraper.Contracts;

// Generic interface that all runners must implement
public interface IRunner
{
  Task Run(string url, string downloadedName);
}