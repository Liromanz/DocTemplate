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
        /// <summary>
        /// GET запрос на сервер
        /// </summary>
        /// <param name="urlName">Ссылка на запрос</param>
        /// <returns>Ответ в виде JSON</returns>
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

        /// <summary>
        /// Пустой POST запрос на сервер
        /// </summary>
        /// <param name="urlName">Ссылка на запрос</param>
        /// <returns></returns>
        public static async Task<string> PostRequest(string urlName)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync($"{GlobalConstants.UrlBase}/{urlName}", null);
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

        /// <summary>
        /// POST запрос на сервер
        /// </summary>
        /// <param name="urlName">Ссылка на запрос</param>
        /// <param name="toSend">Тело запроса в виде JSON</param>
        /// <returns>Ответ в виде JSON</returns>
        public static async Task<string> PostWithBodyRequest(string urlName, string toSend)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(toSend, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{GlobalConstants.UrlBase}/{urlName}", content);
                response.EnsureSuccessStatusCode();
                return GlobalConstants.SuccessMessage;
            }
            catch (Exception e)
            {
                return GlobalConstants.ErrorMessage + e.Message;
            }
        }

        /// <summary>
        /// PUT запрос на сервер
        /// </summary>
        /// <param name="urlName">Ссылка на запрос</param>
        /// <param name="id">ID для измененияс</param>
        /// <param name="needToAddIdToUrl">Нужно ли добавлять ID к ссылке</param>
        /// <param name="dataToSend">Тело запроса в виде JSON</param>
        /// <returns>Ответ в виде JSON</returns>
        public static string PutRequest(string urlName, int id, bool needToAddIdToUrl, string dataToSend)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpContent content = new StringContent(dataToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync($"{GlobalConstants.UrlBase}/{urlName}{(needToAddIdToUrl ? $"/{id}" : "")}", content).Result;
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

        /// <summary>
        /// DELETE запрос на сервер
        /// </summary>
        /// <param name="urlName">Ссылка на запрос</param>
        /// <param name="id">ID для измененияс</param>
        /// <returns>Ответ в виде JSON</returns>
        public static string DeleteRequest(string urlName, int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.DeleteAsync($"{GlobalConstants.UrlBase}/{urlName}/{id}").Result;
                return response.StatusCode.ToString();
            }
            catch (Exception e)
            {
                return GlobalConstants.ErrorMessage + e.Message;

            }
        }
    }
}
