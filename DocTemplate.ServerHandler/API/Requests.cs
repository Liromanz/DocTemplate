using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocTemplate.Global;

namespace DocTemplate.ServerHandler.API
{
    public class Requests
    {
        public static async Task<string> GetRequest(string tableName)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync($"{GlobalConstants.UrlBase}/{tableName}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
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

        public static async Task<string> PostWithBodyRequest(string tableName, string jsonToSend)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{GlobalConstants.UrlBase}/{tableName}", content);
                response.EnsureSuccessStatusCode();
                return GlobalConstants.SuccessMessage;
            }
            catch (Exception e)
            {
                return GlobalConstants.ErrorMessage + e.Message;
            }
        }

        public static async Task<string> PutRequest(string tableName, string dataToSend)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{GlobalConstants.UrlBase}/{tableName}", content);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Conflict:
                        return GlobalConstants.ConflictMessage;
                    case HttpStatusCode.NotFound:
                        return GlobalConstants.NotFoundMessage;
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
        public static async Task<string> DeleteRequest(string tableName, int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync($"{GlobalConstants.UrlBase}/{tableName}/{id}");
                return response.StatusCode.ToString();
            }
            catch (Exception e)
            {
                return GlobalConstants.ErrorMessage + e.Message;

            }
        }
    }
}
