using System.Net.Http;
using System.Threading.Tasks;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
    }

    public async void ApiTextCall(object sender, RoutedEventArgs e)
    {
        string apiUrl = "https://api.opendota.com/api/matches/7401120722";
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read and display the content
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    DisplayApiResponse(apiResponse);
                }
                else
                {
                    // Handle the error
                    DisplayApiResponse($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                DisplayApiResponse($"Error: {ex.Message}");
            }
        }
    }
}

