using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace HackathonProject.Services
{
    public class DatabricksService : IDatabricksService
    {
        private readonly HttpClient _httpClient;
        private readonly DatabricksSettings _settings;

        public DatabricksService(IOptions<DatabricksSettings> options)
        {
            _settings = options.Value;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _settings.Token);
        }

        private async Task<string> ExecuteQueryAsync(string sql)
        {
            var payload = new
            {
                statement = sql,
                warehouse_id = _settings.WarehouseId
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_settings.BaseUrl}/api/2.0/sql/statements", content);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<List<recert_users>> GetusersAsync()
        {
            string sql = "SELECT id, name FROM your_schema.employee";
            var json = await ExecuteQueryAsync(sql);

            dynamic result = JsonConvert.DeserializeObject(json);
            var list = new List<recert_users>();

            foreach (var row in result.result.data_array)
            {
                list.Add(new recert_users
                {
                    id = row[0],
                    name = row[1]
                });
            }

            return list;
        }

        public async Task<List<recert_applications>> GetapplicationsAsync()
        {
            string sql = "SELECT id, application_name FROM your_schema.recert_applications";
            var json = await ExecuteQueryAsync(sql);

            dynamic result = JsonConvert.DeserializeObject(json);
            var list = new List<recert_applications>();

            foreach (var row in result.result.data_array)
            {
                list.Add(new recert_applications
                {
                    id = row[0],
                    application_name = row[1]
                });
            }

            return list;
        }

        public async Task<List<recert_user_access>> Getuser_accessAsync()
        {
            string sql = "SELECT user_id, application_id FROM your_schema.user_access";
            var json = await ExecuteQueryAsync(sql);

            dynamic result = JsonConvert.DeserializeObject(json);
            var list = new List<recert_user_access>();

            foreach (var row in result.result.data_array)
            {
                list.Add(new recert_user_access
                {
                    user_id = row[0],
                    application_id = row[1]
                });
            }

            return list;
        }

        public async Task<List<recert_requests>> GetrequestsAsync()
        {
            string sql = "SELECT timesheet_id, employee_id, date, hours_worked FROM your_schema.recert_requests";
            var json = await ExecuteQueryAsync(sql);

            dynamic result = JsonConvert.DeserializeObject(json);
            var list = new List<recert_requests>();

            foreach (var row in result.result.data_array)
            {
                list.Add(new recert_requests
                {
                    request_id = row[0],
                    user_id = row[1],
                    application_id = row[2],
                    ai_recommendation = row[3]
                });
            }

            return list;
        }
    }
}
