namespace APIForTennis.Models;

public class Players
{
    public int Id { get; set; } = 0;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    [JsonProperty("sex")]
    internal Sex MySex { get; set; } = Sex.Male;
    private string _displayMySex = Sex.Male.ToString();
    public string DisplayMySex 
    {
        get => MySex.ToString().MySexToString();
        private set
        {
            _displayMySex = value;
        }
    }
    [JsonProperty("country")]
    public Country MyCountry { get; set; } = new Country();
    public Uri Picture { get; set; } = new Uri("https://MafyouIT.tech");
    [JsonProperty("data")]
    public LeaderBoard MyLeaderBoard { get; set; } = new LeaderBoard();
}