using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DatabaseConnection.Models
{
  public class WettkampfModel
  {
    public int? WettkampfID { get; set; }
    public TimeSpan Datum { get; set; }
    public string Name { get; set; }
    public int StreckenID { get; set; }
  }
}
