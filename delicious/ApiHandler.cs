namespace se.dsve.Starapi.api;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

using System;
using System.Net.Http;
using System.Threading.Tasks;

public class ApiHandler
{
    private readonly HttpClient httpClient;

    public ApiHandler()
    {
        // Todo: Create a new HttpClient
        httpClient = new HttpClient();
        // Set a timeout of 30 seconds
        httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public async Task<string?> SendRequestAsync(string url)
    {
        // Todo: Create a new HttpRequest with try/catch and return the body of the response
        try
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }

    public async Task<JObject?> GetDataAsync(string url)
    {
        // Todo: Take the response from SendRequest and create a JObject of it if it's not null, otherwise return null
        try
        {
            HttpResponseMessage jsonResponse = await httpClient.GetAsync(url);

            jsonResponse.EnsureSuccessStatusCode();
            string response = await jsonResponse.Content.ReadAsStringAsync();

            JObject? responseJobject = JObject.Parse(response);
            return responseJobject;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }
}