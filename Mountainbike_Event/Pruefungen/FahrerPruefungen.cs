using Service.DatabaseConnection;
using Service.DatabaseConnection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_duper_ding.Pruefungen
{
  public class FahrerPruefungen
  {
    public static Pruefergebnis CheckIfFahrerAlreadyExists(FahrerModel fahrer, IDatabaseConnector cnn)
    {
      var isValid = cnn.ZeigeAlleFahrerAsync().Result.Where(x => x.VName == fahrer.VName && x.NName == fahrer.NName && x.Plz == fahrer.Plz && x.Strasse == fahrer.Strasse).FirstOrDefault() == null;

      return new Pruefergebnis
      {
        IsValid = isValid,
        Fehlertext = isValid ? "" : "Fahrer ist bereits vorhanden !"
      };
    }

    internal static Pruefergebnis CheckIfSelectedteamExists(FahrerModel fahrer, IDatabaseConnector cnn)
    {
      var teams = cnn.ZeigeAlleTeamsAsync().Result;
     var isValid = teams.Where(x => x.TeamID == fahrer.TeamID).Count() > 0;


      return new Pruefergebnis
      {
        IsValid = isValid,
        Fehlertext = isValid ? "" : "Ausgewähltes Team existiert nicht "
      };
    }
  }
}
