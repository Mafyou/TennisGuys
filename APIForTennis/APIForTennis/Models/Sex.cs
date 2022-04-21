using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace APIForTennis.Models;

[JsonConverter(typeof(StringEnumConverter))]
public enum Sex
{
    [EnumMember(Value ="M")]
    Male,
    [EnumMember(Value = "F")]
    Female
}
