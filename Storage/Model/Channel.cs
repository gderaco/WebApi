using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

[Table("Channels")]
public class Channel
{
    [Key]
    [JsonProperty(PropertyName="id")]
    public string Id { get; set; }

    [JsonProperty(PropertyName="name")]
    public string Name { get; set; }
}