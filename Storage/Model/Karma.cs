using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

public class Karma
{
    [Key]
    [JsonProperty(PropertyName="name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName="score")]
    public int Score { get; set; }
}