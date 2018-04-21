using System.Collections.Generic;

namespace Integration.Tools.Abstract
{
    public interface IApiHttpClient
    {
        T GetDeleteRequest<T>(string url, bool isDeleteRequest, Dictionary<string, string> additionalHealders = null);

        TReturnDto PostPutRequest<TReturnDto, TPostDto>(string url, TPostDto data, bool isPutRequest,
            Dictionary<string, string> additionalHeaders = null);
    }
}