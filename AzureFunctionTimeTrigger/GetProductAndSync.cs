using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;

namespace AzureFunctionTimeTrigger
{
    public static class GetProductAndSync
    {
        [FunctionName("GetProductAndSync")]
        public static void Run([TimerTrigger("*/30 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            using (SqlConnection conn = new SqlConnection("<your database connection string>"))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT COUNT(*) as count FROM PRODUCTS";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        log.LogInformation("##############################################");
                        log.LogInformation($"PRODUCTS QUANTITY: {reader["count"]}");
                        log.LogInformation("##############################################");
                    }
                }
            }
        }
    }
}
