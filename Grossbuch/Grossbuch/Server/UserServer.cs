using Grossbuch.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Grossbuch.Server
{
    class UserServer
    {
        //const string Url = "http://15520.pythonanywhere.com/api-token-auth/";
        //readonly string Token = "";

        public UserServer()
        {

        }

        //private HttpClient GetClient()
        //{
        //    HttpClient client = new HttpClient();
        //    client.DefaultRequestHeaders.Add("Content-Type", "application/json");
        //    return client;
        //}

        private class LoginModel
        {
            public string username = "", password = "", token = "";

            public LoginModel(string _username, string _password)
            {
                username = _username;
                password = _password;
            }
        }

        public static async Task<User> LoginAsync(string username, string password)
        {
            string url = "http://10.0.2.2:8000/api-token-auth/";
            LoginModel loginModel = new LoginModel(username, password);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            try
            {
                var response = await client.PostAsync(url,
                new StringContent(
                    JsonConvert.SerializeObject(loginModel),
                    Encoding.UTF8, "application/json"));

                if (response.StatusCode != HttpStatusCode.OK)
                    return null;

                var user = JsonConvert.DeserializeObject<LoginModel>(
                await response.Content.ReadAsStringAsync());

                return await Task.FromResult(new User(loginModel.username, loginModel.password, user.token));
            }
            catch (Exception e)
            {
                Console.WriteLine(e + "!!!");
                return null;
            }
        }

        //public static async Task<User> LoginAsync(string username, string password)
        //{
        //    string url = "http://15520.pythonanywhere.com/api-token-auth/";
        //    LoginModel loginModel = new LoginModel(username, password);
        //    HttpClient client = new HttpClient();
        //    client.DefaultRequestHeaders.Add("Accept", "application/json");

        //    try
        //    {

        //        var response = await client.PostAsync(url,
        //        new StringContent(
        //            JsonConvert.SerializeObject(loginModel),
        //            Encoding.UTF8, "application/json"));

        //        if (response.StatusCode != HttpStatusCode.OK)
        //            return null;

        //        var user = JsonConvert.DeserializeObject<LoginModel>(
        //        await response.Content.ReadAsStringAsync());

        //        return await Task.FromResult(new User(loginModel.username, loginModel.password));
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e + "!!!");
        //        return null;
        //    }
        //}

        //private static async Task<Account[]> LoadData()
        //{
        //    try
        //    {
        //        HttpClient client = new HttpClient { BaseAddress = new Uri("http://127.0.0.1:8000/accounts/") };
        //        var response = await client.GetAsync(client.BaseAddress);
        //        response.EnsureSuccessStatusCode(); // выброс исключения, если произошла ошибка

        //        // десериализация ответа в формате json
        //        var content = await response.Content.ReadAsStringAsync();
        //        JObject o = JObject.Parse(content);

        //        var str = o.SelectToken(@"$.results");
        //        var accounts = JsonConvert.DeserializeObject<Account[]>(str.ToString());
        //        return await Task.FromResult(accounts);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception: " + ex);
        //        return null;
        //    }
        //}
    }
}
