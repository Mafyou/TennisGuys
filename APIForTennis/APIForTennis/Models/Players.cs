using APIForTennis.Helpers;
using Newtonsoft.Json;

namespace APIForTennis.Models;

public class Players
{
    public int Id { get; set; } = 0;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    [JsonProperty("sex")]
    public Sex MySex { get; set; } = Sex.Male;
    [JsonProperty("country")]
    public Country MyCountry { get; set; } = new Country();
    public Uri Picture { get; set; } = new Uri("https://MafyouIT.tech");
    [JsonProperty("data")]
    public LeaderBoard MyLeaderBoard { get; set; } = new LeaderBoard();
}