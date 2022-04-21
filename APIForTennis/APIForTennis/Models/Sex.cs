namespace APIForTennis.Models;

[JsonConverter(typeof(StringEnumConverter))]
public enum Sex
{
    [EnumMember(Value = "M")]
    Male,
    [EnumMember(Value = "F")]
    Female
}