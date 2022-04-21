namespace APIForTennis.Models;

public class LeaderBoard
{
    public int Rank { get; set; } = 0;
    public int Points { get; set; } = 0;
    public int Weight { get; set; } = 0;
    public int Height { get; set; } = 0;
    public int Age { get; set; } = 0;
    public int[] Last { get; set; } = new int[5];
}