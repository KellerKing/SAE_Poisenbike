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
    Task<List<FahrerModel>> ZeigeAlleFahrerAsync();
    Task<int> AddFahrerToDatabaseAsync(FahrerModel fahrer);

    Task<List<TeamModel>> ZeigeAlleTeamsAsync();
    Task<int> AddTeamToDatabaseAsync(TeamModel team);

    Task<List<FahrerModel>> GetFahrerImTeam(TeamModel team); //Doch lieber mit linq in der Logik ?

    Task<List<StreckenModel>> ZeigeAlleStreckenAsync();
    Task<int> CreateStrecke(StreckenModel strecke);

    Task<List<WettkampfModel>> ZeigeAlleWettkaempfeAsync();
    Task<int> CreateWettkampfAsync(WettkampfModel wettkampf);


    Task<int> InsertErreichteZeit(BestenlisteModel bestenliste);

    Task<List<BestenlisteModel>> GetBestenlisteProWettkmapf(WettkampfModel wettkampf);

  }
}