using CsvHelper.Configuration.Attributes;
using Scraper.Utils;

namespace Scraper.Models;

public class KinderMorganRaw
{
  [Name("TSP")]
  public int TspId { get; set; }

  [Name("TSP Name")]
  public string TspName { get; set; } = string.Empty;

  [Name("TSP FERC CID")]
  public string TspFercId { get; set; } = string.Empty;

  [Ignore]
  public DateTime DownloadDate { get; set; } = DateTime.Now;

  [Ignore]
  public string Comments { get; set; } = string.Empty;

  [Name("Loc")]
  public int LocationNumber { get; set; }

  [Name("Loc Name")]
  public string LocationName { get; set; } = string.Empty;

  [Name("Dir Flo")]
  public string DirFlo { get; set; } = string.Empty;

  [Name("Loc Cnty")]
  public string LocationCounty { get; set; } = string.Empty;

  [Name("Loc St Abbrev")]
  public string State { get; set; } = string.Empty;

  [Name("Loc Type Ind")]
  public string LocationType { get; set; } = string.Empty;

  [Name("Loc Zone (Rec)")]
  public string LocationZoneRec { get; set; } = string.Empty;

  [Name("Loc Zone (Del)")]
  public string LocationZoneDel { get; set; } = string.Empty;

  [Name("Seg Nbr")]
  public int SegNumber { get; set; } = 0;

  [Name("Nom Ind")]
  public string NomIndString { get; set; }

  [Ignore]
  public bool NomIndicator => NomIndString == "Y" ? true : false;

  [Name("Loc Stat Ind")]
  public string LocationStatIndex { get; set; } = string.Empty;

  [Name("Eff Date")]
  public string EffectiveDateString { get; set; } = string.Empty;

  [Ignore]
  public DateTime? EffectiveDate => DateUtils.ParseDateString(EffectiveDateString, "yyyyMMdd");

  [Name("Inact Date")]
  public string InactiveDateString { get; set; } = string.Empty;

  [Ignore]
  public DateTime? InactiveDate => DateUtils.ParseDateString(InactiveDateString, "yyyyMMdd");

  [Name("Up/Dn Ind")]
  public string UpDnIndString { get; set; } = string.Empty;

  [Ignore]
  public bool UpDnInd => UpDnIndString == "Y" ? true : false;

  [Name("Up/Dn Name")]
  public string UpDnName { get; set; } = string.Empty;

  [Name("Up/Dn ID")]
  public int? UpDnId { get; set; }

  [Name("Up/Dn ID Prop")]
  public int? UpDnIdProp { get; set; }

  [Name("Up/Dn FERC CID Ind")]
  public string UpDnFercCidIndString { get; set; } = string.Empty;

  [Name("Up/Dn FERC CID")]
  public string UpDnFercCid { get; set; } = string.Empty;

  [Ignore]
  public bool UpDnFercCidInd => UpDnFercCidIndString == "Y" ? true : false;

  [Name("Up/Dn Loc")]
  public int? UpDnLocation { get; set; }

  [Name("Up/Dn Loc Name")]
  public string UpDnLocationName { get; set; } = string.Empty;

  [Name("Up/Dn Loc 2")]
  public string UpDnLocation2 { get; set; } = string.Empty;

  [Name("Up/Dn Loc Name2")]
  public string UpDnLocation2Name { get; set; } = string.Empty;

  [Name("Update D/T")]
  public string UpdateDateTimeString { get; set; } = string.Empty;

  [Ignore]
  public DateTime? UpdateDateTime => DateUtils.ParseDateString(UpdateDateTimeString, "yyyyMMdd HH:mm");
}