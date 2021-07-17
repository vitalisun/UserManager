using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UserManager.BL.Models;
using UserManager.DAL.Entities;

namespace UserManager.BL.Services.Implementations
{
    public class ConsoleService
    {
        const string url = @"https://localhost:44323";

        public UserCredentials UserCredentials;

        public ConsoleService()
        {
            UserCredentials = new UserCredentials();
        }

        public void Login()
        {
            if (string.IsNullOrEmpty(UserCredentials.Login) ||
                string.IsNullOrEmpty(UserCredentials.Password))
            {
                Console.WriteLine("Необходима авторизация");

                Console.WriteLine("Введите логин:");
                UserCredentials.Login = Console.ReadLine();

                Console.WriteLine("Введите пароль:");
                UserCredentials.Password = Console.ReadLine();
            }
        }

        public void Logout()
        {
            UserCredentials.Login = String.Empty;
            UserCredentials.Password = String.Empty;
        }

        public async Task<string> CreateUserRequestAsync(UserInfo userInfo)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserInfo));

            string xmlInput = "";
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, userInfo);
                xmlInput = textWriter.ToString();
            }
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
              AuthenticationSchemes.Basic.ToString(),
              Convert.ToBase64String(Encoding.ASCII.GetBytes($"{UserCredentials.Login}:{UserCredentials.Password}"))
              );

            HttpResponseMessage response = await httpClient.PostAsync($"{url}/Auth/CreateUser", new StringContent(xmlInput, Encoding.Unicode, "application/xml"));

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                Logout();

            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

        public async Task<string> RemoveUserRequestAsync(int id)
        {
            RemoveUserRequest request = new RemoveUserRequest { RemoveUser = new RemoveUser { Id = id } };

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
              AuthenticationSchemes.Basic.ToString(),
              Convert.ToBase64String(Encoding.ASCII.GetBytes($"{UserCredentials.Login}:{UserCredentials.Password}"))
              );

            string jsonInput = JsonSerializer.Serialize(request);

            var response = await httpClient.PostAsync($"{url}/Auth/RemoveUser",
                new StringContent(jsonInput, Encoding.Unicode, "application/json"));

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                Logout();

            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

        public async Task<string> SetStatusRequest(string id, string status)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
              AuthenticationSchemes.Basic.ToString(),
              Convert.ToBase64String(Encoding.ASCII.GetBytes($"{UserCredentials.Login}:{UserCredentials.Password}"))
              );

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("Id", id));
            keyValues.Add(new KeyValuePair<string, string>("NewStatus", status));


            var response = await httpClient.PostAsync($"{url}/Auth/SetStatus", new FormUrlEncodedContent(keyValues));

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                Logout();

            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

        public async Task<string> UserInfoRequest(int id)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync($"{url}/Public/UserInfo?id={id}");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                Logout();

            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

        public async Task<List<UserInfo>> GetUsersAsync()
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync($"{url}/Public/GetUsers");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                Logout();

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<UserInfo>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
