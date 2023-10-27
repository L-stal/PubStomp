using Microsoft.UI.Xaml;
using System.Net.Http;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DotaApi
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }
        public async void ApiTextCall(object sender, RoutedEventArgs e)
        {
            // Your API endpoint
            string apiUrl = "https://api.opendota.com/api/matches/7401120722";

            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Make the GET request
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

        private void DisplayApiResponse(string response)
        {
            // Assuming you have a TextBlock named "responseTextBlock" in your XAML
            responseTextBlock.Text = response;
        }
    }
}
