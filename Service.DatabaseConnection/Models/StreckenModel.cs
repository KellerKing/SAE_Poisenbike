using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DatabaseConnection.Models
{
  public class StreckenModel
  {
    public int? StreckenID { get; set; }
    public float Distanz_KM { get; set; }
    public int Höhenmeter { get; set; }
    public decimal Startgeld { get; set; }
    public string Name { get; set; } = "undefined";
  }
}
