using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Stackexchange.Application.DTOs.Tag;

namespace Stackexchange.Tests;

public static class ConnectionHandler
{
    public static async Task<List<TagGet>?> GetApiTags(string url)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        using var result = await client.GetAsync(url);
        if (!result.IsSuccessStatusCode) throw new Exception(result.StatusCode.ToString());
        var str = await result.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<List<TagGet>>(str);
        
        return response;
    }
    
    public static async Task<HttpStatusCode> GetApiHttpCode(string url, bool get)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        if (get)
        {
            using var result = await client.GetAsync(url);
            return result.StatusCode;
        }
        else
        {
            using var result = await client.PostAsync(url, null);
            return result.StatusCode;
        }
    }
}