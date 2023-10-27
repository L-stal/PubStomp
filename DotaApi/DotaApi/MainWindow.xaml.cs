using Microsoft.UI.Xaml;
using DotaApi.ApiManger;
using Newtonsoft.Json.Linq;

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
            JArray players = (JArray)jsonResponse["players:personaname"];
            JArray heroes = (JArray)jsonResponse["hero_id"];

            string heroesString = "Heroes:\n" + string.Join(", ", heroes);
            string playersString = "Players:\n" + string.Join(", ", players);

            namesAndHeroesTextBox.Text = playersString + "\n" + heroesString;
        }
    }
}
