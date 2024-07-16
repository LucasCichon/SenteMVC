using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Services;
using Microsoft.Extensions.Configuration;
using Sente.Domain.Interfaces;
using Sente.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sente.Infrastructure.Repositories
{
    public class WorklogRepository : IWorklogRepository
    {
        private readonly string _connectionString;

        public WorklogRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("FirebirdConnection");
        }

        public async Task<IEnumerable<WorklogEntry>> GetWorklogsAsync(DateTime fromDate, DateTime toDate)
        {
            var worklogs = new List<WorklogEntry>();
            try
            {

            using (var connection = new FbConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new FbCommand("SELECT * FROM X_JIRA_WORKLOG WHERE WORKLOGDATE BETWEEN @FromDate AND @ToDate", connection))
                {
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@ToDate", toDate);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            worklogs.Add(new WorklogEntry
                            {
                                Ref = reader.GetInt32(0),
                                Author = reader.GetString("AUTHOR"),
                                Project = reader.GetString(2),
                                Issue = reader.GetString(3),
                                IssueSummary = reader.GetString(4),
                                Qualification = reader.GetString(5),
                                TimeSpent = reader.GetDouble(6),
                                WorklogDate = reader.GetDateTime(7),
                                WorklogStartTimestamp = reader.GetDateTime(8),
                                RegTimestamp = reader.GetDateTime(9),
                                Descrip = reader.GetString(10),
                                Components = reader.GetString(11),
                            });
                        }
                    }
                }
            }
            }
            catch(Exception ex)
            {
                var test = ex.Message;
            }

            return worklogs;
        }
    }
}
