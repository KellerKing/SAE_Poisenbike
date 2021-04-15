using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DatabaseConnection.Models
{
  public class TeamModel
  {
    public int? TeamID { get; set; }
    public string Teamname { get; set; }
    public string Email { get; set; }
    public string Plz { get; set; }
    public string Hausnummer { get; set; }
    public string Strasse { get; set; }
    public string Ort { get; set; }
  }
}
