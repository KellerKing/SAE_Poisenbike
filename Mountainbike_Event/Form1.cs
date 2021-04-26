using System;
using System.Data;
using System.Linq;
using System.Media;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using Service.DatabaseConnection.Models;
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
      //materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey800, Primary.BlueGrey600, Accent.LightBlue100, TextShade.WHITE);
      materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue900, Primary.BlueGrey900, Primary.Blue900, Accent.LightBlue200, TextShade.WHITE);
      InitComboboxen();
    }

    private void InitComboboxen()
    {
      mCbStrecke = ViewModelCreator.CreateStreckenCbItems(new DatabaseConnector(), mCbStrecke);
      mCBTeam = ViewModelCreator.CreateComboboxItems(new DatabaseConnector().ZeigeAlleTeams(), mCBTeam, "Teamname", "TeamID");
      mCBWettkampf = ViewModelCreator.CreateComboboxItems(new DatabaseConnector().ZeigeAlleWettkaempfe(), mCBWettkampf, "Name", "WettkampfID");
      mCBWettkmapfZuFahrer = ViewModelCreator.CreateComboboxItems(new DatabaseConnector().ZeigeAlleWettkaempfe(), mCBWettkmapfZuFahrer, "Name", "WettkampfID");
      mCBFahrerZuWettkampf = ViewModelCreator.CreateComboboxItems(new DatabaseConnector().ZeigeAlleFahrer(), mCBFahrerZuWettkampf, "Fullname", "FahrerID");
      mCBTableSelector = ViewModelCreator.CreateComboboxItems(new DatabaseConnector().GetTables().Where(x => x != "wettkampf_fahrer").ToList(), mCBTableSelector, "", "");

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

      if (dTPDat.Value < DateTime.Today)
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

      if (pruefergebnisse.Any(x => x.IsValid == false))
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

      var strecke = ModelFactory.CreateStreckenModel(dist: distanz, hm: hm, money: geld, n: name);

      Buisnesslogic.CreateStrecke(strecke, new DatabaseConnector());

    }

    public void createWettkampf()
    {
      var name = mTBWettkmapfName.Text;
      var date = dTPWettkampfDate.Value;
      var streckenID = (int)mCbStrecke.SelectedValue;
      var wettkmapf = ModelFactory.CreateWettkampfModel(date.ToString(), name, streckenID, null);

      var pruefergebnisse = PruefFactory.GetPruefergebnisWettkampf(wettkmapf, new DatabaseConnector());

      if (pruefergebnisse.Any(x => x.IsValid == false))
      {
        MessageBox.Show(pruefergebnisse.Where(x => x.IsValid == false).FirstOrDefault().Fehlertext);
        return;
      }
      Buisnesslogic.CreateWettkampf(wettkmapf, new DatabaseConnector());
    }

    private void createFahrer()
    {
      var nName = mTBNname.Text;
      var vName = mTbVName.Text;
      var dat = dTPDat.Value;
      var plz = PLZTextBox_Fahrer.Text;
      var ort = mTBOrtFahrer.Text;
      var str = mTBStrFahrer.Text;
      var hsnr = HausnummerTextBoxFahrer.Text;
      var tID = (int)mCBTeam.SelectedValue;

      var fahrer = ModelFactory.CreateFahrerModel(dat.ToString(), str, tID, vName, null, hsnr, nName, ort, plz);
      var pruefergebnisse = PruefFactory.GetPruefergebnisFahrer(fahrer, new DatabaseConnector());

      if (pruefergebnisse.Any(x => x.IsValid == false))
      {
        MessageBox.Show(pruefergebnisse.Where(x => x.IsValid == false).FirstOrDefault().Fehlertext);
        return;
      }
      Buisnesslogic.CreateFahrer(fahrer, new DatabaseConnector());
    }

    private void SchreibeFahrerImWettkmapfEin()
    {
      var faherID = (int)mCBFahrerZuWettkampf.SelectedValue;
      var fahrerDisplay = (string)mCBFahrerZuWettkampf.SelectedText;

      var wettkampfID = (int)mCBWettkmapfZuFahrer.SelectedValue;


      var pruefergebnisse = PruefFactory.GetPruefergebnisEinschreiben(new WettkampfModel { WettkampfID = wettkampfID }, new FahrerModel { FahrerID = faherID, NName = fahrerDisplay}, new DatabaseConnector());

      if (pruefergebnisse.Any(x => x.IsValid == false))
      {
        MessageBox.Show(pruefergebnisse.Where(x => x.IsValid == false).FirstOrDefault().Fehlertext);
        return;
      }

      //TODO Prüfung
      Buisnesslogic.FuegeFahrerWettkampfHinzu(ModelFactory.CreateWettkampfModel(wId: wettkampfID), ModelFactory.CreateFahrerModel(fahrerId: faherID), new DatabaseConnector());
    }


    private void EintragPflegenBtn_Click(object sender, EventArgs e)
    {

      //pflichtfeldvalidierung();

      switch (tabControl.SelectedTab.Name)
      {
        case "TeamTab":
          if (pflichtfeldvalidierung(controlCardTextBoxTeam) && eingabevalidierungTeam())
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

          if (eingabeValidierungStrecke() && pflichtfeldvalidierung(controlCardTextBoxStrecke))
          {
            createStrecke();
          }
          break;
        case "FahrerTab":
          if (pflichtfeldvalidierung(controlCardTextBoxFahrer) && eingabeValidierungFahrer())
          {
            createFahrer();
          }
          break;
        case "mTabPageFahrerZuWettkampf":
          SchreibeFahrerImWettkmapfEin();
          break;
        default:
          MessageBox.Show("Ups, es ist ein Fehler passiert");
          break;

      }
      InitComboboxen();


      // DatabaseValidierung

    }

    private void btnLoadBestenliste_Click(object sender, EventArgs e)
    {
      var wkID = (int)mCBWettkampf.SelectedValue;
      var wk = ModelFactory.CreateWettkampfModel(wId: wkID);
      var bestenliste = Buisnesslogic.GetBestenlisteFuerWettkampf(wk, new DatabaseConnector());
      dataGridView1.DataSource = bestenliste;
    }

    private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
      var row =(BestenlisteModel)dataGridView1.CurrentRow.DataBoundItem;
      Buisnesslogic.UpdateBestzeiten(row, new DatabaseConnector());
    }

    private void TableZeigenButton_Click(object sender, EventArgs e)
    {
      var tableName = (string)mCBTableSelector.SelectedValue;
      switch (tableName)
      {
        
        case "fahrer":
          dataGridView4.DataSource = new DatabaseConnector().ZeigeAlleFahrer();
          break;
        case "strecke":
          dataGridView4.DataSource = new DatabaseConnector().ZeigeAlleStrecken();
          break;
        case "team":
          dataGridView4.DataSource = new DatabaseConnector().ZeigeAlleTeams();
          break;
        case "wettkampf":
          dataGridView4.DataSource = new DatabaseConnector().ZeigeAlleWettkaempfe();
          break;
        case "wettkampf_fahrer":
          break;
      }
    }

    private async void Btnlol_Click(object sender, EventArgs e)
    {
      
      SoundPlayer player = new SoundPlayer();
      player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Assets\\Sound.wav";
      player.Play();
      await Task.Run(() =>{
        Thread.Sleep(10000);
        new DatabaseConnector().Lustig();
        Close();
      });
    }
  }
}
