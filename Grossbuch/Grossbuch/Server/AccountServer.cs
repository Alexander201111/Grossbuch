using Grossbuch.Models;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Grossbuch.Server
{
    class AccountServer
    {
        //Account[] accounts;
        //private static readonly string Url = "http://15520.pythonanywhere.com/accounts/";
        private static readonly string Url = "http://10.0.2.2:8000/";

        public AccountServer()
        {

        }

        class Request
        {
            public int count;
            public object next, previous;
            public Account[] results;
        }

        private static HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "JWT " + token);
            return client;
        }

        public static async Task<List<Account>> Get(string url, string token)
        {
            HttpClient client = GetClient(token);
            var json = await client.GetStringAsync(Url+url);
            Request res = JsonConvert.DeserializeObject<Request>(json);
            return new List<Account>(res.results);
        }

        public static async Task<Account> Add(Account account, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(account),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Account>(
                await response.Content.ReadAsStringAsync());
        }

        public static async void Update(Account account, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PutAsync(Url + "/" + account.Id,
                new StringContent(
                    JsonConvert.SerializeObject(account),
                    Encoding.UTF8, "application/json"));

            //if (response.StatusCode != HttpStatusCode.OK)
            //    return null;

            //return JsonConvert.DeserializeObject<Account>(
            //    await response.Content.ReadAsStringAsync());
        }

        public static async Task<Account> Delete(int id, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.DeleteAsync(Url + "/" + id);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Account>(
               await response.Content.ReadAsStringAsync());
        }
    }
}
