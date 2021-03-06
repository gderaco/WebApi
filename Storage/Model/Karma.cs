using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

[Table("Karmas")]
public class Karma
{
    [Key]
    [JsonProperty(PropertyName="id")]
    public string Id { get; set; }

    [JsonProperty(PropertyName="name")]
    public string Name { get; set; }

    [JsonProperty(PropertyName="score")]
    public int Score { get; set; }

    [JsonProperty(PropertyName="channelId")]
    [Column(TypeName="TEXT")]
    public string ChannelId { get; set; }
}