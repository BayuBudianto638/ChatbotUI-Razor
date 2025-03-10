using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebApplication1.Models;

public class ChatMessageModel
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("user")]
    public string User { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }
}

public class ChatResponse
{
    [JsonProperty("message")]
    public List<ChatMessageModel> Messages { get; set; }
}