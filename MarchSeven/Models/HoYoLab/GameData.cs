﻿using System.Text.Json.Serialization;

namespace MarchSeven.Models.HoYoLab;

public class GameData
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("type")]
    public int? Type { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }
}
