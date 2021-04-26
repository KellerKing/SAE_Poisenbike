using Service.DatabaseConnection;
using Service.DatabaseConnection.Models;
using Super_duper_ding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mountainbike_Event.Pruefungen
{
  public class TeilnahmePruefungen
  {
    public static Pruefergebnis CheckIfFahrerNimmtTeil(FahrerModel fahrer, WettkampfModel wk, IDatabaseConnector cnn)
    {
      var bl = cnn.GetBestenlisteProWettkmapf(wk);
      var isValid = bl.Where(x => x.FahrerID == fahrer.FahrerID).Count() == 0;

      return new Pruefergebnis
      {
        IsValid = isValid,
        Fehlertext = isValid ? "" : $"Der Fahrer {fahrer.Fullname} nimmt bereits am Wettkmapf teil"
      };
    }
  }
}
