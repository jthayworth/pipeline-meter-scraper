namespace Scraper.Models;

public class TableItem
{
  public int TspId { get; set; }
  public string TspName { get; set; } = string.Empty;
  public string FercCid { get; set; } = string.Empty;
  public string UpDownFercCid { get; set; } = string.Empty;
  public DateTime DownloadDate { get; set; }
  public int LocationNumber { get; set; }
  public int? UpDownLocationNumber { get; set; }
  public string LocationName { get; set; } = string.Empty;
  public string DirFlo { get; set; } = string.Empty;
  public string County { get; set; } = string.Empty;
  public string State { get; set; } = string.Empty;
  public string ZoneRec { get; set; } = string.Empty;
  public string ZoneDel { get; set; } = string.Empty;
  public int SegNumber { get; set; }
  public bool NomIndicator { get; set; }
  public string StatIndicator { get; set; } = string.Empty;
  public DateTime? EffectiveDate { get; set; }
  public DateTime? InactiveDate { get; set; }
  public DateTime? UpdateDateTime { get; set; }

  public TableItem()
  {

  }

  public TableItem(KinderMorganRaw rawItem)
  {
    TspId = rawItem.TspId;
    TspName = rawItem.TspName;
    FercCid = rawItem.TspFercId;
    UpDownFercCid = rawItem.UpDnFercCid;
    DownloadDate = rawItem.DownloadDate;
    LocationNumber = rawItem.LocationNumber;
    UpDownLocationNumber = rawItem.UpDnLocation;
    LocationName = rawItem.LocationName;
    DirFlo = rawItem.DirFlo;
    County = rawItem.LocationCounty;
    State = rawItem.State;
    ZoneRec = rawItem.LocationZoneRec;
    ZoneDel = rawItem.LocationZoneDel;
    SegNumber = rawItem.SegNumber;
    NomIndicator = rawItem.NomIndicator;
    StatIndicator = rawItem.LocationStatIndex;
    EffectiveDate = rawItem.EffectiveDate;
    InactiveDate = rawItem.InactiveDate;
    UpdateDateTime = rawItem.UpdateDateTime;
  }
}