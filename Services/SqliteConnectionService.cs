using System.Data;
using Microsoft.Data.Sqlite;

namespace TheOregonTrailAI.Services;

public class SqliteConnectionService
{
    private readonly string _connectionString;
    public SqliteConnectionService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection GetConnection()
    {
        return new SqliteConnection(_connectionString);
    }

}