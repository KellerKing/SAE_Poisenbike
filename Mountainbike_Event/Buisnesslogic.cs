using Mountainbike_Event;
using Service.DatabaseConnection;
using Service.DatabaseConnection.Models;
using Super_duper_ding.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Super_duper_ding
{
  public class Buisnesslogic //TODO: Oder bekommt das nur prim. Params und erstellt die Models selber ?
  {
    public static List<BestenlisteModel> GetBestenlisteFuerWettkampf(WettkampfModel wettkampf, IDatabaseConnector db)
    {
      var bestenlisten = db.GetBestenlisteProWettkmapf(wettkampf);
      var fahrer = db.ZeigeAlleFahrer();

      bestenlisten.ForEach(x => {
        x.Fahrer = fahrer.Where(f => f.FahrerID == x.FahrerID).FirstOrDefault();
      });

      return bestenlisten;
    }

    public static int UpdateBestzeiten(BestenlisteModel eintrag, IDatabaseConnector db)
    {
      //TODO: Vorher auch eigentlich prüfen !
      //Temporär daher
      //if (updatedBestzeit == 0) return updatedBestzeit;

      return db.InsertErreichteZeit(eintrag);
    }

    public static int CreateTeam(TeamModel team, IDatabaseConnector db)
    {
      return db.AddTeamToDatabaseAsync(team).Result;
    }

    public static int CreateStrecke(StreckenModel strecke, IDatabaseConnector db)
    {
      return db.CreateStrecke(strecke).Result;
    }

    internal static int CreateWettkampf(WettkampfModel wettkmapf, IDatabaseConnector databaseConnector)
    {
      return databaseConnector.CreateWettkampf(wettkmapf);
    }

    public static int CreateFahrer(FahrerModel fahrer, IDatabaseConnector db)
    {
      return db.AddFahrerToDatabase(fahrer);
    }

    internal static int FuegeFahrerWettkampfHinzu(WettkampfModel wettkampfModel, FahrerModel fahrerModel, IDatabaseConnector db)
    {
      var dieBestenlisten = db.GetBestenlisteProWettkmapf(wettkampfModel);
      var startnummer = dieBestenlisten.Max(x => x.Fahrer_startnummer) + 1 ?? 1;
      var neuerEintrag = ModelFactory.CreateBestenlisteModel(wettkampfModel, fahrerModel, startnummer);
      return db.TrageFahrerInWettkampfEin(neuerEintrag);
    }

    public static List<T> GetDatabseTableData<T>(string tableName, IDatabaseConnector db) 
    {

      return db.GetTableDataByName<T>(tableName);

     
    }
  }
}
