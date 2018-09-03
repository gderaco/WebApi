using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class KarmaRequest
{
    [JsonProperty(PropertyName="karma")]
    public Karma Karma { get; set; }
    [JsonProperty(PropertyName="action")]
    public KarmaAction Action { get; set; }
    [JsonProperty(PropertyName="channel")]
    public string Channel { get; set; }
}

[JsonConverter(typeof(StringEnumConverter))]
public enum KarmaAction
{
    [JsonProperty(PropertyName="increment")]
    Increment,
    [JsonProperty(PropertyName="decrement")]
    Decrement
}