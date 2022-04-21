using APIForTennis.Models;

namespace APIForTennis.Helpers;

public static class MyExtensions
{
    public static string MySexToString(this string mySex)
    {
        if (mySex == "Male") return "Man";
        return "Female";
    }
    public static double AddMediane(this PlayersWithMediane playerOne, PlayersWithMediane playerTwo)
    {
        return playerOne.Size + playerTwo.Size;
    }
}