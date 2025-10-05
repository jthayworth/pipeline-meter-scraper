using Scraper.Models;

namespace Scraper.Contracts;

// Generic interface for SQL operations
public interface ISql
{
  Task InsertData(IEnumerable<TableItem> items);
}

