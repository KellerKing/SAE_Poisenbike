using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DatabaseConnection.Models
{
  public class FahrerModel
  {
    public int? FahrerID { get; set; }
    public string VName { get; set; }
    public string Plz { get; set; }
    public string NName { get; set; }
    public string Hausnummer { get; set; }
    public string Strasse { get; set; }
    public string Ort { get; set; }
    public DateTime GebDat { get; set; }
    public int TeamID { get; set; }
  }
}
