using AzureFunctionsBusTrigger.Entity;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace AzureFunctionsBusTrigger
{
    public static class GetMessageAndSave
    {
        [FunctionName("GetMessageAndSave")]
        public static void Run([ServiceBusTrigger("az-204-test", Connection = "AzureServiceBusConnection")] string message, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {message}");

            var product = JsonConvert.DeserializeObject<Product>(message);

            using (SqlConnection conn = new SqlConnection("<your storage connection string>"))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Products(Name, Price, ImageUrl) VALUES(@param2, @param3, @param4)";

                cmd.Parameters.AddWithValue("@param2", product.Name);
                cmd.Parameters.AddWithValue("@param3", product.Price);
                cmd.Parameters.AddWithValue("@param4", product.ImageUrl);

                var rows = cmd.ExecuteNonQuery();
            }
        }
    }
}
