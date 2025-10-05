using Dapper;
using Scraper.Contracts;
using Scraper.Models;

namespace Scraper.Implementation;

public class Sql(DbSettings settings) : ISql
{
  // Inserts a collection of TableItem records into the database. This is not the most efficient way, but that can
  // be optimized later if needed.
  public async Task InsertData(IEnumerable<TableItem> items)
  {
    using var connection = new Npgsql.NpgsqlConnection($"{settings.BaseConnection}Username={settings.Username};Password={settings.Password};");
    await connection.OpenAsync();

    using var transaction = connection.BeginTransaction();

    try
    {
      foreach (TableItem item in items)
      {
        await connection.ExecuteAsync(
          @"
            INSERT INTO public.table_item 
            ( 
              tsp_id, 
              tsp_name, 
              ferc_cid, 
              up_down_ferc_cid, 
              download_date, 
              location_number, 
              up_down_location_number, 
              location_name, 
              dir_flo, 
              county, 
              state, 
              zone_rec, 
              zone_del, 
              seg_number, 
              nom_indicator, 
              stat_indicator, 
              effective_date, 
              inactive_date, 
              update_date_time 
            ) 
            VALUES 
            ( 
              @TspId, 
              @TspName, 
              @FercCid, 
              @UpDownFercCid, 
              @DownloadDate, 
              @LocationNumber, 
              @UpDownLocationNumber, 
              @LocationName, 
              @DirFlo, 
              @County, 
              @State, 
              @ZoneRec, 
              @ZoneDel, 
              @SegNumber, 
              @NomIndicator, 
              @StatIndicator, 
              @EffectiveDate, 
              @InactiveDate, 
              @UpdateDateTime 
            )
          ",
          item,
          transaction: transaction
        );
      }

      await transaction.CommitAsync();
    }
    catch (System.Exception)
    {
      await transaction.RollbackAsync();
      throw;
    }
  }
}