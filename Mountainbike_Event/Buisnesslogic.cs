using Service.DatabaseConnection;
using Service.DatabaseConnection.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Super_duper_ding
{
  public class Buisnesslogic //TODO: Oder bekommt das nur prim. Params und erstellt die Models selber ?
  {
    public static List<BestenlisteModel> GetBestenlisteFuerWettkampf(WettkampfModel wettkampf, IDatabaseConnector db)
    {
      var bestenlisten = db.GetBestenlisteProWettkmapf(wettkampf).Result;
      var fahrer = db.ZeigeAlleFahrerAsync();

      bestenlisten.ForEach(x => {
        x.Fahrer = fahrer.Result.Where(f => f.FahrerID == x.FahrerID).FirstOrDefault();
      });

      return bestenlisten;
    }

    public static int UpdateBestzeiten(WettkampfModel wettkampfModel, FahrerModel fahrer, TimeSpan zeit,IDatabaseConnector db)
    {
      var updatedBestzeit = db.GetBestenlisteProWettkmapf(wettkampfModel).Result.Where(x => x.FahrerID == fahrer.FahrerID).FirstOrDefault();
      //TODO: Vorher auch eigentlich prüfen !
      //Temporär daher
      //if (updatedBestzeit == 0) return updatedBestzeit;

      updatedBestzeit.Zeit = zeit;
      return db.InsertErreichteZeit(updatedBestzeit).Result;
    }

    public static int CreateTeam(TeamModel team, IDatabaseConnector db)
    {
      return db.AddTeamToDatabaseAsync(team).Result;
    }
  }
}
