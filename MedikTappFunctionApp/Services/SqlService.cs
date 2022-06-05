using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Threading.Tasks;

namespace MedikTappFunctionApp.Services
{
    public class SqlService
    {
        private readonly ILogger<SqlService> _logger;

        public SqlService(ILogger<SqlService> logger) => _logger = logger;

        public async Task<SqlDataReader> ExecuteStoredProcedure(string spName, params (string, object)[] kvp)
        {
            try
            {
                var sqlConnection = new SqlConnection(Environment.GetEnvironmentVariable("SQLServerConnectionString"));
                using var cmd = new SqlCommand(spName, sqlConnection) { CommandType = CommandType.StoredProcedure };

                foreach ((var parameterName, var value) in kvp)
                    cmd.Parameters.Add(new(parameterName, value));

                await sqlConnection.OpenAsync();
                _logger.LogInformation($"Executed stored procedure: {spName}");
                return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                _logger.LogError($"A problem happened in ExecuteStoredProcedure method, see the returned response for more information: {ex.Message}");
                return default;
            }
        }
    }
}