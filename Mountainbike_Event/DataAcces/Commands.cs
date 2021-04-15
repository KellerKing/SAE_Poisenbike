using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_duper_ding.SQL_Commands
{
  public class Commands
  {
    public static string[] procedures = new string[]
    {
      "getFahrer",
      "insertFahrer",
      "insertTeam",
      "getWettkaempfe",
      "insertWettkampf",
      "getStrecken",
      "insertStrecke",
      "getTeams",
      "updateErreichteZeit",
      "getAlleBestenlistenProWettkampf",
    };
  }


  public enum CommandNames : UInt16
  {
    getFahrer = 0,
    insertFahrer = 1,
    insertTeam = 2,
    getWettkaempfe = 3,
    insertWettkampf = 4,
    getStrecken = 5,
    insertStrecke = 6,
    getTeams = 7,
    updateErreichteZeit = 8,
    getAlleBestenlistenProWettkampf = 9,
  }
}
