using Service.DatabaseConnection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DatabaseConnection
{
  public interface IDatabaseConnector
  {
    List<FahrerModel> ZeigeAlleFahrer();
    int AddFahrerToDatabase(FahrerModel fahrer);

    List<TeamModel> ZeigeAlleTeams();
    Task<int> AddTeamToDatabaseAsync(TeamModel team);

    Task<List<FahrerModel>> GetFahrerImTeam(TeamModel team); //Doch lieber mit linq in der Logik ?

    List<StreckenModel> ZeigeAlleStrecken();
    Task<int> CreateStrecke(StreckenModel strecke);

    List<WettkampfModel> ZeigeAlleWettkaempfe();
    int CreateWettkampf(WettkampfModel wettkampf);


    Task<int> InsertErreichteZeit(BestenlisteModel bestenliste);

    List<BestenlisteModel> GetBestenlisteProWettkmapf(WettkampfModel wettkampf);

    int TrageFahrerInWettkampfEin(BestenlisteModel bestenliste);

  }
}