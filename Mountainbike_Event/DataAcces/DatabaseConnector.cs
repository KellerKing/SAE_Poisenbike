using Dapper;
using Service.DatabaseConnection;
using Super_duper_ding.SQL_Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.DatabaseConnection.Models;
using MySqlConnector;

namespace Super_duper_ding.DataAcces
{
  public class DatabaseConnector : IDatabaseConnector
  {

    private MySqlConnection _connection;
    //server=127.0.0.1;user=root;password=1234;database=test

    public DatabaseConnector() //TODO: Später mit übergebenem Objekt
    {
      _connection = new MySqlConnection("server=127.0.0.1;user=root;password=1234;database=poisenbike_cup");
      //_connection = mySqlConnection;
    }

    public async Task<int> AddFahrerToDatabaseAsync(FahrerModel fahrer)
    {
      var procedure = Commands.procedures[(int)CommandNames.insertFahrer];

      using (var clone = (MySqlConnection)_connection.Clone())
      {
        clone.Open();
        return clone.Execute(procedure, fahrer, commandType: System.Data.CommandType.StoredProcedure);
      }


    }

    public async Task<List<FahrerModel>> ZeigeAlleFahrerAsync()
    {
      var procedure = Commands.procedures[(int)CommandNames.getFahrer];

      var alleFahrer = new List<FahrerModel>();

      using (var clone = (MySqlConnection)_connection.Clone())
      {
        alleFahrer = (List<FahrerModel>)await clone.QueryAsync<FahrerModel>(procedure, null, commandType: System.Data.CommandType.StoredProcedure);
      }

      return alleFahrer;
    }

    public async Task<List<TeamModel>> ZeigeAlleTeamsAsync()
    {
      var procedure = Commands.procedures[(int)CommandNames.getTeams];

      var clone = _connection.Clone();
      using (clone)
      {
        clone.Open();
        return (List<TeamModel>)await clone.QueryAsync<TeamModel>(procedure, null, commandType: System.Data.CommandType.StoredProcedure);
      }

    }

    public async Task<int> AddTeamToDatabaseAsync(TeamModel team)
    {
      var procedure = Commands.procedures[(int)CommandNames.insertTeam];

      using (var clone = _connection.Clone())
      {
        clone.Open();
        var rows = clone.Execute(procedure, team, commandType: System.Data.CommandType.StoredProcedure); //TODO:
        return rows;
      }
    }

    public Task<List<FahrerModel>> GetFahrerImTeam(TeamModel team)
    {
      throw new System.NotImplementedException();
    }

    public List<StreckenModel> ZeigeAlleStrecken()
    {
      var procedure = Commands.procedures[(int)CommandNames.getStrecken];
      using (var clone = (MySqlConnection)_connection.Clone())
      {
        clone.Open();
        return (List<StreckenModel>) clone.Query<StreckenModel>(procedure, null, commandType: System.Data.CommandType.StoredProcedure);
      }

    }

    public async Task<int> CreateStrecke(StreckenModel strecke)
    {
      var procedure = Commands.procedures[(int)CommandNames.insertStrecke];

      using (var clone = (MySqlConnection)_connection.Clone())
      {
        clone.Open();
        return clone.Execute(procedure, strecke, commandType: System.Data.CommandType.StoredProcedure);
      }
    }

    public List<WettkampfModel> ZeigeAlleWettkaempfe()
    {
      var procedure = Commands.procedures[(int)CommandNames.getWettkaempfe];

      using (var clone = (MySqlConnection)_connection.Clone())
      {
        return (List<WettkampfModel>) clone.Query<WettkampfModel>(procedure, null, commandType: System.Data.CommandType.StoredProcedure);
      }
    }

    public int CreateWettkampf(WettkampfModel wettkampf)
    {

      var procedure = Commands.procedures[(int)CommandNames.insertWettkampf];

      using (var clone = (MySqlConnection)_connection.Clone())
      {
        clone.Open();
        return clone.Execute(procedure, wettkampf, commandType: System.Data.CommandType.StoredProcedure);
      }

    }

    public async Task<int> InsertErreichteZeit(BestenlisteModel bestenliste)
    {
      var procedure = Commands.procedures[(int)CommandNames.updateErreichteZeit];

      using (var clone = (MySqlConnection)_connection.Clone())
      {
        clone.Open();
        return clone.Execute(procedure, bestenliste, commandType: System.Data.CommandType.StoredProcedure);
      }
    }

    public async Task<List<BestenlisteModel>> GetBestenlisteProWettkmapf(WettkampfModel wettkampf)
    {
      var procedure = Commands.procedures[(int)CommandNames.getAlleBestenlistenProWettkampf];

      using (var clone = (MySqlConnection)_connection.Clone())
      {
        return (List<BestenlisteModel>) await clone.QueryAsync<BestenlisteModel>(procedure, null, commandType: System.Data.CommandType.StoredProcedure);
      }

    }


    //

  }
}
