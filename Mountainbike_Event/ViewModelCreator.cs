using MaterialSkin.Controls;
using Service.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ComboBox;

namespace Mountainbike_Event
{
  class ViewModelCreator
  {
    public static MaterialComboBox CreateStreckenCbItems(IDatabaseConnector db, MaterialComboBox cb)
    {
      cb.DataSource = db.ZeigeAlleStrecken();
      cb.DisplayMember = "Name";
      cb.ValueMember = "StreckenID";
      return cb;
    }
    //TODO: Kann zusammengefasst werden !

    public static MaterialComboBox CreateComboboxItems<T>(List<T> datasource, MaterialComboBox cb, string displayAttr, string valueAttr)
    {
      cb.DataSource = datasource;
      cb.DisplayMember = displayAttr;
      cb.ValueMember = valueAttr;
      return cb;
    }

  }
}
