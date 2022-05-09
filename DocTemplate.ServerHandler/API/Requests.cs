using DocTemplate.Global;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocTemplate.ServerHandler.API
{
    public class Requests
    {
        public static string GetRequest(string urlName)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync($"{GlobalConstants.UrlBase}/{urlName}").Result;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {
                return GlobalConstants.ErrorMessage + e.Message;
            }
        }

        public static async Task<string> PostRequest(string tableName)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync($"{GlobalConstants.UrlBase}/{tableName}", null);
                response.EnsureSuccessStatusCode();
                if (response.StatusCode == HttpStatusCode.Created)
                    return await response.Content.ReadAsStringAsync();
                return GlobalConstants.UnsetErrorMessage;
            }
            catch (Exception e)
            {
                return GlobalConstants.ErrorMessage + e.Message;
            }
        }

        public static async Task<string> PostWithBodyRequest(string tableName, string toSend)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(toSend, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{GlobalConstants.UrlBase}/{tableName}", content);
                response.EnsureSuccessStatusCode();
                return GlobalConstants.SuccessMessage;
            }
            catch (Exception e)
            {
                return GlobalConstants.ErrorMessage + e.Message;
            }
        }

        public static string PutRequest(string tableName, int id, bool needToAddIdToUrl, string dataToSend)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync($"{GlobalConstants.UrlBase}/{tableName}{(needToAddIdToUrl ? $"/{id}" : "")}", content).Result;
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Conflict:
                        return GlobalConstants.ConflictMessage;
                    case HttpStatusCode.NotFound:
                        return GlobalConstants.NotFoundMessage;
                    case HttpStatusCode.NoContent:
                        return GlobalConstants.SuccessMessage;
                    case HttpStatusCode.OK:
                        return GlobalConstants.SuccessMessage;
                    default:
                        return GlobalConstants.UnsetErrorMessage;
                }
            }
            catch (Exception e)
            {
                return GlobalConstants.ErrorMessage + e.Message;
            }
        }
        public static string DeleteRequest(string tableName, int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.DeleteAsync($"{GlobalConstants.UrlBase}/{tableName}/{id}").Result;
                return response.StatusCode.ToString();
            }
            catch (Exception e)
            {
                return GlobalConstants.ErrorMessage + e.Message;

            }
        }
    }
}
