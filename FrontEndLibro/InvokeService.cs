using FrontEndLibro.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FrontEndLibro
{
    public class InvokeService
    {
        public static Response Get(string urlBase, string parametros)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(urlBase);
            client.DefaultRequestHeaders.Accept.Clear();
            var url = $"{urlBase}?id={parametros}";
            var message = client.GetAsync(url);
            message.Wait();
            var response_ = message.Result;

            if (!response_.IsSuccessStatusCode)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = response_.StatusCode.ToString(),
                };
            }

            var result = response_.Content.ReadAsStringAsync();
            var modelResult = result.Result;
            Response response = new Response();
            response.Message = modelResult;
            response.IsSuccess = true;
            return response;
        }


        public static Response Post(string urlBase, object obj, string parametros="")
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(urlBase);
            client.DefaultRequestHeaders.Accept.Clear();
            var url = $"{urlBase}";

            JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
            jsonSettings.NullValueHandling = NullValueHandling.Ignore;
            var myContent = JsonConvert.SerializeObject(obj, jsonSettings);

            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var llamada = client.PostAsync(url, byteContent);
            llamada.Wait();
            var response_ = llamada.Result;
            var modelResult = response_.Content.ReadAsStringAsync().Result;

            if (!response_.IsSuccessStatusCode)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = modelResult,
                    ResultType = response_.StatusCode.ToString()
                };
            }
            else
            {
                Response respuesta = new Response();
                respuesta = JsonConvert.DeserializeObject<Response>(modelResult);
                respuesta.ResultType = response_.StatusCode.ToString();
                return respuesta;
            }
        }
    }
}
