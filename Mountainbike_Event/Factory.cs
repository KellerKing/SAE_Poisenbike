using Service.DatabaseConnection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mountainbike_Event
{
  public class Factory
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
  }
}
