using Grossbuch.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Grossbuch.Server
{
    class OperationServer
    {
        //private static readonly string Url = "http://15520.pythonanywhere.com/accounts/";
        private static readonly string Url = "http://10.0.2.2:8000/";

        public OperationServer() { }

        class Request
        {
            public int count;
            public object next, previous;
            public Operation[] results;
        }

        private static HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "JWT " + token);
            return client;
        }

        public static async Task<List<Operation>> Get(string url, string token)
        {
            HttpClient client = GetClient(token);
            var json = await client.GetStringAsync(Url + url);
            Request res = JsonConvert.DeserializeObject<Request>(json);
            return new List<Operation>(res.results);
        }

        public static async Task<Operation> Add(Operation account, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(account),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Operation>(
                await response.Content.ReadAsStringAsync());
        }

        public static async void Update(Operation account, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PutAsync(Url + "/" + account.Id,
                new StringContent(
                    JsonConvert.SerializeObject(account),
                    Encoding.UTF8, "application/json"));
        }

        public static async Task<Operation> Delete(int id, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.DeleteAsync(Url + "/" + id);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Operation>(
               await response.Content.ReadAsStringAsync());
        }
    }
}
