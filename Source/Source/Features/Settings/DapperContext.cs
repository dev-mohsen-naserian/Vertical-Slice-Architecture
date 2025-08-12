using Microsoft.Data.SqlClient;
using System.Data;

namespace Source.Features.Settings;
 public class DapperContext
   {
    private readonly IConfiguration _configuration;
    private readonly string _connectionString = string.Empty;
    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("SQLConnection");
    }
    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
   }

