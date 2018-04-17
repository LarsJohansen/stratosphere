using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Integration.Tools
{
    public class ApiHttpClient
    {
        public T GetDeleteRequest<T>(string url, bool isDeleteRequest = false)
        {
            var clientHandler = CreateClientHandler();
            using (var httpClient = new HttpClient(clientHandler))
            {

                httpClient.DefaultRequestHeaders.Accept.Clear();

                var response = (isDeleteRequest) ? httpClient.DeleteAsync(url).Result : httpClient.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                {
                    ThrowRestApiException(response);
                }

                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        public TReturnDto PostPutRequest<TReturnDto, TPostDto>(TPostDto data, string url, bool isPutRequest = false)
        {
            var clientHandler = CreateClientHandler();
            using (var httpClient = new HttpClient(clientHandler))
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonContent = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = (isPutRequest) ? httpClient.PutAsync(url, content).Result : httpClient.PostAsync(url, content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    ThrowRestApiException(response);
                }

                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<TReturnDto>(result);
            }
        }


        private static void ThrowRestApiException(HttpResponseMessage response)
        {
            var errorResponse = response.Content.ReadAsStringAsync().Result;
            var errorResult =
                JsonConvert.DeserializeObject<RestApiErrorMessage>(errorResponse);
            throw new RestApiException { ReturnCode = errorResult.statusCode.ToString(), ReturnMessage = errorResult.Message };
        }

        private static HttpClientHandler CreateClientHandler()
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.UseDefaultCredentials = true;
            clientHandler.PreAuthenticate = true;
            clientHandler.ClientCertificateOptions = ClientCertificateOption.Automatic;
            return clientHandler;
        }
    }
}
