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
        public T GetDeleteRequest<T>(string url,  bool isDeleteRequest, Dictionary<string, string> additionalHealders = null)
        {
  
            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                AddAdditionalHeaders<T>(additionalHealders, httpClient);

                var response = (isDeleteRequest) ? httpClient.DeleteAsync(url).Result : httpClient.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                {
                    ThrowRestApiException(response);
                }

                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        private static void AddAdditionalHeaders<T>(Dictionary<string, string> additionalHealders, HttpClient httpClient)
        {
            if (additionalHealders != null)
            {
                foreach (var additionalHeader in additionalHealders)
                {
                    httpClient.DefaultRequestHeaders.Add(additionalHeader.Key, additionalHeader.Value);
                }
            }
        }

        public TReturnDto PostPutRequest<TReturnDto, TPostDto>(TPostDto data, string url, bool isPutRequest = false)
        {
     
            using (var httpClient = new HttpClient())
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
            var errorResponse = "";
            RestApiErrorMessage restApiErrorMessage;
            try
            {
                 errorResponse = response.Content.ReadAsStringAsync().Result;
                 restApiErrorMessage =
                    JsonConvert.DeserializeObject<RestApiErrorMessage>(errorResponse);
            }
            catch (Exception ex)
            {

                throw new Exception($"Could not deserialize into RestApiException\nErrorResponse: {errorResponse}\n" +
                                    $"Exception: {ex}");
            }
            throw new RestApiException { ReturnCode = restApiErrorMessage.statusCode.ToString(), ReturnMessage = restApiErrorMessage.Message };
        }

       
    }
}
