using System.Globalization;
using CsvHelper;

namespace Scraper.Utils;

public static class CsvUtils
{
  // Generic method to parse CSV file into a list of objects of type T
  public static List<T> ParseCsv<T>(string filePath)
  {
    using var reader = new StreamReader(filePath);
    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
    var records = csv.GetRecords<T>();
    return records.ToList();
  }
}