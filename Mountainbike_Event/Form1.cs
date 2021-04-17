using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using Super_duper_ding;
using Super_duper_ding.DataAcces;

namespace Mountainbike_Event
{
  public partial class Form1 : MaterialForm

  {

    private DatabaseConnector _db;

    public Form1()
    {
      InitializeComponent();

      _db = new DatabaseConnector();

      var materialSkinManager = MaterialSkinManager.Instance;

      materialSkinManager.AddFormToManage(this);
      materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
      materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey800, Primary.BlueGrey600, Accent.LightBlue100, TextShade.WHITE);


      InitComboboxen();
    }

    private void InitComboboxen()
    {
      mCbStrecke = ViewModelCreator.CreateStreckenCbItems(new DatabaseConnector(), mCbStrecke);
    }


    private bool pflichtfeldvalidierung(Control Controlcard)
    {

      foreach (var control in Controlcard.Controls)
      {

        MaterialTextBox t = control as MaterialTextBox;

        if (t?.Text == String.Empty)
        {
          MessageBox.Show("Bitte füllen sie alle Felder aus! :)");
          return false;

        }
      }
      return true;
    }

    private bool eingabevalidierungTeam()
    {
      // E-Mail
      if (Regex.IsMatch(E_MailTextBox.ToString(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
      {
        return false;
      }
      //PLZ
      else if (Regex.IsMatch(PLZ_LabelTeam.ToString(), @"/[0-9]{5}/"))
      {
        return false;

      }
      //Hausnummer
      else if (Regex.IsMatch(HausnummerTextBoxTeam.ToString(), @"/[0-9]*$/"))
      {
        return false;
      }

      return true;


    }

    private bool eingabeValidierungStrecke()
    {
      if (Regex.IsMatch(StartgeldTextBox.ToString(), @"/[0 - 9] *$/"))
      {
        return false;
      }
      return true;
    }

    private bool eingabeValidierungFahrer()
    {
      
      if (dateTimePicker2.Value < DateTime.Today)
      {
        MessageBox.Show("Bitte geben Sie ein gültiges Datum ein !");
        return false;
      }
      else if (Regex.IsMatch(PLZTextBox_Fahrer.ToString(), @"/[0-9]{5}/"))
      {
        return false;
      }
      else if (Regex.IsMatch(HausnummerTextBoxFahrer.ToString(), @"/[0-9]*$/"))
      {
        return false;
      }

      return true;

    }

    private void createTeam()
    {
      var team = ModelFactory.CreateTeamModel(E_MailTextBox.Text, HausnummerTextBox.Text, materialTextBoxOrt.Text, PLZTextBoxTeam.Text, materialTextBoxStr.Text, null, materialTextBoxTeamname.Text);
      var pruefergebnisse = PruefFactory.GetPruefergebnisTeam(team, new DatabaseConnector());
      
      if(pruefergebnisse.Any(x=> x.IsValid == false))
      {
        MessageBox.Show(pruefergebnisse.Where(x => x.IsValid == false).FirstOrDefault().Fehlertext);
        return;
      }
      Buisnesslogic.CreateTeam(team, new DatabaseConnector());


    }

    public void createStrecke()
    {
      var geld = Convert.ToDecimal(StartgeldTextBox.Text);
      var hm = Convert.ToInt32(mTbHoehe.Text);
      var distanz = (float)Convert.ToDouble(mtbDistanz.Text);
      var name = mTBStreckeName.Text;

      var strecke = ModelFactory.CreateStreckenModel( dist: distanz, hm: hm, money: geld, n: name);

      Buisnesslogic.CreateStrecke(strecke, new DatabaseConnector());

    }

    public void createWettkampf()
    {
      var name = mTBWettkmapfName.Text;
      var time = dTPWettkampfDate.Value;
      var streckenID = (int)mCbStrecke.SelectedValue;
      var wettkmapf = ModelFactory.CreateWettkampfModel(time, name, streckenID, null);

      var pruefergebnisse = PruefFactory.GetPruefergebnisWettkampf(wettkmapf, new DatabaseConnector());

      if (pruefergebnisse.Any(x => x.IsValid == false))
      {
        MessageBox.Show(pruefergebnisse.Where(x => x.IsValid == false).FirstOrDefault().Fehlertext);
        return;
      }
      Buisnesslogic.CreateWettkampf(wettkmapf, new DatabaseConnector());
    }


    private void EintragPflegenBtn_Click(object sender, EventArgs e)
    {
      
      //pflichtfeldvalidierung();

      switch (materialTabControl1.SelectedTab.Name)
      {
        case "TeamTab":
          if(pflichtfeldvalidierung(controlCardTextBoxTeam) && eingabevalidierungTeam())
          {
            createTeam();
          }
          
          break;
        case "WettkampfTab":
          if (pflichtfeldvalidierung(controlCardTextBoxWettkampf))
          {
            createWettkampf();
          }
          break;
        case "StreckeTab":
          
          if(eingabeValidierungStrecke() && pflichtfeldvalidierung(controlCardTextBoxStrecke))
          {
            createStrecke();
          }
          break;
        case "FahrerTab":
          pflichtfeldvalidierung(controlCardTextBoxFahrer);
          eingabeValidierungFahrer();
          break;
        default:
          MessageBox.Show("Ups, es ist ein Fehler passiert");
          break;

      }

    

      // DatabaseValidierung

    }
  }
}
