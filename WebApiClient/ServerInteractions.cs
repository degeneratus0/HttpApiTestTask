using System.Net;
using System.Text;
using System.Text.Json;
using WebApi.Models.DTOs;

namespace WebApiClient
{
    internal static class ServerInteractions
    {
        private static readonly Dictionary<string, string> config;
        private static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        private static readonly HttpClient _httpClient = new HttpClient();

        static ServerInteractions()
        {
            string text = File.ReadAllText(@"./config.json");
            config = JsonSerializer.Deserialize<Dictionary<string, string>>(text) ?? new Dictionary<string, string>();
        }

        public static async Task<List<UserDTO>?> Get()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(config["ServerUrl"]);
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();

            List<UserDTO>? usersList = JsonSerializer.Deserialize<List<UserDTO>>(responseContent, jsonSerializerOptions);
            return usersList;
        }

        public static async Task<UserDTO?> Get(string id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{config["ServerUrl"]}/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("Пользователь не найден");
                return null;
            }
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<UserDTO>(responseContent, jsonSerializerOptions);
        }

        public static async Task Post(UserDTO userDTO)
        {
            string json = JsonSerializer.Serialize(userDTO);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(config["ServerUrl"], content);
            response.EnsureSuccessStatusCode();
            Console.WriteLine("Post выполнен успешно");
        }

        public static async Task Put(string id, UserDTO userDTO)
        {
            string json = JsonSerializer.Serialize(userDTO);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync($"{config["ServerUrl"]}/{id}", content);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("Пользователь не найден");
                return;
            }
            response.EnsureSuccessStatusCode();
            Console.WriteLine("Put выполнен успешно");
        }

        public static async Task Delete(string id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{config["ServerUrl"]}/{id}");
            if (id == string.Empty)
            {
                Console.WriteLine("Указан некорректный номер");
                return;
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("Пользователь не найден");
                return;
            }
            response.EnsureSuccessStatusCode();
            Console.WriteLine("Delete выполнен успешно");
        }
    }
}
