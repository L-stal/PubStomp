using Microsoft.UI.Xaml;
using DotaApi.ApiManger;
using Newtonsoft.Json.Linq;
using System.Text;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DotaApi
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private readonly ApiManager apiManager;
        public MainWindow()
        {
            this.InitializeComponent();
            apiManager = new ApiManager();
        }

        private async void ApiTextCall(object sender, RoutedEventArgs e)
        {
            string matchData = await apiManager.GetMatch();
            DisplayMatchData(matchData);
        }

        private void DisplayMatchData(string text)
        {
            allMatchDataTextBox.Text = text;
        }
        private async void GetNamesAndHeros(object sender, RoutedEventArgs e)
        {
            string namesAndHeros = await apiManager.GetMatch();
            DisplayNamesAndHeros(namesAndHeros);

        }
        private void DisplayNamesAndHeros(string text)
        {
            JObject jsonResponse = JObject.Parse(text);

            if (jsonResponse["players"] != null && jsonResponse["players"].HasValues)
            {
                StringBuilder sb = new StringBuilder();
                foreach (JToken player in jsonResponse["players"])
                {
                    string playerName = (string)player["personaname"];
                    if (playerName == null) 
                    {
                        playerName = "NNF";
                    }
                    string heroId = (string)player["hero_id"];
                    sb.AppendLine($"{playerName}\n{heroId}");

                }
                namesAndHeroesTextBox.Text = sb.ToString();
            }
            else
            {
                namesAndHeroesTextBox.Text = "No player data found.";
            }
        }
    }
}
