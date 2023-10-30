using Microsoft.UI.Xaml;
using DotaApi.ApiManger;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System;

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
        private async void DisplayNamesAndHeros(string text)
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
                        playerName = "Private Account";
                    }
                    int heroId;
                    if (int.TryParse(player["hero_id"].ToString() , out heroId))
                    {
                        JToken hero = await GetHeroById(heroId);
                        if ( hero != null)
                        {
                            string heroName = (string)hero["localized_name"];
                            sb.AppendLine($"Player: {playerName} || Hero: {heroName}");
                        }
                    }

                }
                namesAndHeroesTextBox.Text = sb.ToString();
            }
            else
            {
                namesAndHeroesTextBox.Text = "No player data found.";
            }
        }
        private async Task<JToken> GetHeroById(int heroId) 
        {
            try
            {
                var json = await File.ReadAllTextAsync("C:/Users/leost/Desktop/Code map/Dota2OpenAPI/DotaApi/DotaApi/Data/Heroes.json");
                JObject heroes = JObject.Parse(json);
                JToken hero = heroes[heroId.ToString()];
                return hero;
            } 
            catch {
                Console.WriteLine("No json file found {ex.Message}");
                return null;
            }
        }
    }
}
