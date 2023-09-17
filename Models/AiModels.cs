#nullable enable
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace TheOregonTrailAI.Models;

public class AIRequest
{
    public AIRequest(string model = "")
    {
        Messages = new List<AIMessages>();
        Model = "";
    }
    public IEnumerable<AIMessages> Messages { get; set; }
    public double Temperature { get; set; }
    public string Model { get; set; }
}

public class AIMessages
{
    public AIMessages()
    {
        Role = "";
        Message = "";
    }
    public string Role { get; set; }
    public string Message { get; set; }
}

public class AIResponse
{
    public AIResponse()
    {
        ResponseSuccessful = false;
        Response = string.Empty;
    }
    public bool ResponseSuccessful { get; set; }
    public string? Response { get; set; }
}