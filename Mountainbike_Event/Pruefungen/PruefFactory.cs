using Mountainbike_Event.Pruefungen;
using Service.DatabaseConnection;
using Service.DatabaseConnection.Models;
using Super_duper_ding.Pruefungen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_duper_ding
{
  public class PruefFactory
  {
    public static List<Pruefergebnis> GetPruefergebnisTeam(TeamModel team, IDatabaseConnector connector)
    {
      return new List<Pruefergebnis>()
      {
        TeamPruefungen.CheckIfTeamAlreadyExists(team.Teamname, connector)
        //TeamPruefungen.AlleMussfelderAusgefuellt(team)
      };
    }

    public static List<Pruefergebnis> GetPruefergebnisFahrer(FahrerModel fahrer, IDatabaseConnector connector)
    {
      return new List<Pruefergebnis>()
      {
       FahrerPruefungen.CheckIfFahrerAlreadyExists(fahrer, connector),
       FahrerPruefungen.CheckIfSelectedteamExists(fahrer, connector)
      };
    }


    public static List<Pruefergebnis> GetPruefergebnisWettkampf(WettkampfModel wettkampf, IDatabaseConnector connector)
    {
      return new List<Pruefergebnis>()
      {
       WettkmapfPruefungen.CheckIfWettkampfAlreadyExists(wettkampf, connector),
       WettkmapfPruefungen.CheckIfSelectedStreckeExists(wettkampf, connector)
      };
    }

    public static List<Pruefergebnis> GetPruefergebnisEinschreiben(WettkampfModel wettkampf, FahrerModel fahrer, IDatabaseConnector connector)
    {
      return new List<Pruefergebnis>()
      {
        TeilnahmePruefungen.CheckIfFahrerNimmtTeil(fahrer, wettkampf, connector)
      };
    }
  }
}
