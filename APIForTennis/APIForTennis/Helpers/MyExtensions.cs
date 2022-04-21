using APIForTennis.Models;

namespace APIForTennis.Helpers;

public static class MyExtensions
{
    public static string MySexToString(this string mySex)
    {
        if (mySex == "Male") return "Man";
        return "Female";
    }
}