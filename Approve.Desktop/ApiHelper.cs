using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using Approve.Desktop.Models;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using System.ComponentModel;
using Org.BouncyCastle.Tls;
using System.Windows;

namespace Approve.Desktop
{
    public static class ApiHelper
    {
        public static async Task<HttpResponseMessage> Call(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var baseAddress = "https://localhost:44387/";
            string apiUrl = baseAddress + url;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                return client.GetAsync(apiUrl).Result;
            }
        }

        public static async Task<T> GetModelTask<T>(string url)
        {
            var response = await ApiHelper.Call(url);
            var data = response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data.Result);
        }

        public static T GetModel<T>(string url)
        {
            return GetModelTask<T>(url).Result;
        }

        public static async Task<List<T>> GetModelListTask<T>(string url)
        {
            try
            {
                HttpResponseMessage response = await Call(url);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON based on whether it's an array or a single object
                if (jsonResponse.StartsWith("["))
                {
                    return JsonConvert.DeserializeObject<List<T>>(jsonResponse);
                }
                else
                {
                    T singleItem = JsonConvert.DeserializeObject<T>(jsonResponse);
                    return new List<T> { singleItem };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<T>();
            }
        }


        public static IEnumerable<T> GetModelList<T>(string url)
        {
            return GetModelListTask<T>(url).Result.ToList();
        }

        public static async Task PostModel<T>(T model)
        {
            var Address = "https://localhost:44387/";
            if (model.GetType() == typeof(MEmployee))
                Address += "Employees";
            else if (model.GetType() == typeof(MMachine))
                Address += "Machines";
            else if (model.GetType() == typeof(MTeam))
                Address += "Teams";
            else if (model.GetType() == typeof(MRequest))
                Address += "Requests";
            else if (model.GetType() == typeof(MEmail))
                Address += "Emails";

            string json = JsonConvert.SerializeObject(model);
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(Address, content);
            }
        }

        public static async void UpdateModel<T>(T model)
        {
            var Address = "https://localhost:44387/";
            if (model.GetType() == typeof(MEmployee))
                Address += "Employees";
            else if (model.GetType() == typeof(MMachine))
                Address += "Machines";
            else if (model.GetType() == typeof(MTeam))
                Address += "Teams";
            else if (model.GetType() == typeof(MRequest))
                Address += "Requests";
            else if (model.GetType() == typeof(MEmail))
                Address += "Emails";

            string json = JsonConvert.SerializeObject(model);
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(Address, content);
            }
        }

        public static async Task DeleteModel<T>(T model)
        {
            var Address = "https://localhost:44387/";
            if (model.GetType() == typeof(MEmployee))
                Address += "Employees";
            else if (model.GetType() == typeof(MMachine))
                Address += "Machines";
            else if (model.GetType() == typeof(MTeam))
                Address += "Teams";
            else if (model.GetType() == typeof(MRequest))
                Address += "Requests";

            string json = JsonConvert.SerializeObject(model);
            using (HttpClient client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(Address),
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                HttpResponseMessage response = await client.SendAsync(request);
            }
        }
    }
}