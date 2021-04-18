using Service.DatabaseConnection;
using Service.DatabaseConnection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_duper_ding.Pruefungen
{
  public class TeamPruefungen
  {
    public static Pruefergebnis CheckIfTeamAlreadyExists(string name, IDatabaseConnector cnn)
    {
      var isValid = cnn.ZeigeAlleTeams().Where(x => x.Teamname == name).FirstOrDefault() == null;

      return new Pruefergebnis
      {
        IsValid = isValid,
        Fehlertext = isValid ? "" : "Teamname bereits vorhanden !"
      };
    }

    public static Pruefergebnis AlleMussfelderAusgefuellt(TeamModel team)
    {
      var isValid = team.Email != String.Empty && team.Hausnummer != String.Empty && team.Ort != String.Empty && team.Plz != String.Empty && team.Strasse != String.Empty && team.Teamname != String.Empty;

      return new Pruefergebnis
      {
        IsValid = isValid,
        Fehlertext = isValid ? "" : "Bitte füllen Sie alle Felder aus"
      };
    }
  }
}
