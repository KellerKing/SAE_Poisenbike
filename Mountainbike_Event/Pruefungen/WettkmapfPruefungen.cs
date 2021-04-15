using Service.DatabaseConnection;
using Service.DatabaseConnection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_duper_ding.Pruefungen
{
  public class WettkmapfPruefungen
  {
      public static Pruefergebnis CheckIfWettkampfAlreadyExists(WettkampfModel wettkampf, IDatabaseConnector cnn)
      {
        var isValid = cnn.ZeigeAlleWettkaempfeAsync().Result.Where(x => x.Name == wettkampf.Name).FirstOrDefault() == null;

        return new Pruefergebnis
        {
          IsValid = isValid,
          Fehlertext = isValid ? "" : "Wettkämpfe dürfen nicht doppelt vorkommen !"
        };
      }



    public static Pruefergebnis CheckIfSelectedStreckeExists(WettkampfModel wettkampf, IDatabaseConnector cnn)
    {
      var isValid = cnn.ZeigeAlleStreckenAsync().Result.Where(x => x.StreckenID == wettkampf.StreckenID).FirstOrDefault() != null;

      return new Pruefergebnis
      {
        IsValid = isValid,
        Fehlertext = isValid ? "" : "Ausgewählte Strecke existiert nicht !"
      };
    }

  }
}
