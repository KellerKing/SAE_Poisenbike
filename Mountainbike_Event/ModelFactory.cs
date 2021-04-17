using Service.DatabaseConnection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mountainbike_Event
{
  public class ModelFactory
  {
    public static TeamModel CreateTeamModel(string email = "", string hausnummer = "", string ort = "", string plz = "", string strasse = "", int? teamid = null, string teamname = "" )
    {
      return new TeamModel
      {
        Email = email,
        Hausnummer = hausnummer,
        Ort = ort,
        Plz = plz,
        Strasse = strasse,
        TeamID = teamid,
        Teamname = teamname
      };
    }
    public static FahrerModel CreateFahrerModel(DateTime gebDat, string strasse = "", int? teamId = null, string vname = "", int? fahrerId = null, string nummer = "", string nname = "", string ort = "", string plz= "")
    {
      return new FahrerModel
      {
        FahrerID = fahrerId,
        GebDat = gebDat,
        Hausnummer = nummer,
        NName = nname,
        Ort = ort,
        Plz = plz, 
        Strasse = strasse,
        TeamID = teamId,
        VName = vname,
      
      };
    }

    public static WettkampfModel CreateWettkampfModel(DateTime date, string name = "", int? sId = null, int? wId = null)
    {
      return new WettkampfModel
      {
        Datum = date,
        Name = name,
        StreckenID = sId,
        WettkampfID = wId

      };
    }

    public static StreckenModel CreateStreckenModel(float dist = 0, int? sId = null, int hm = 0, Decimal money = 0, string n = "")
    {
      return new StreckenModel
      {
        Distanz_KM = dist,
        StreckenID = sId,
        Höhenmeter = hm,
        Startgeld = money,
        Name = n

      };
    }

    public static BestenlisteModel CreateBestenlisteModel(WettkampfModel wettkampf, FahrerModel fahrer)
    {
      return new BestenlisteModel
      {
       Fahrer = fahrer,
       FahrerID = fahrer.FahrerID,
       Fahrer_startnummer = 0, //TODO: Hier mit Randoms oder so was arbeiten
      wettkampf = wettkampf,
      WettkampfID = wettkampf.WettkampfID,
      };
    }

  }
}
