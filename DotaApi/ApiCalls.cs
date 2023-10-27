using System.Net.Http;
using System.Threading.Tasks;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> GetApiResponse(string apiUrl)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                // Handle error
                return $"Error: {response.StatusCode}";
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            return $"Exception: {ex.Message}";
        }
    }
}

